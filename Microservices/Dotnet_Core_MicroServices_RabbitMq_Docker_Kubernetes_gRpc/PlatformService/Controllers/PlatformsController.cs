using System.Net;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messaegBusClient;

        public PlatformsController(IPlatformRepo repository,
        IMapper mapper,
        ICommandDataClient commandDataClient,
        IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messaegBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms...");

            var platformsLst = _repository.GetAllPlatforms().ToList();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformsLst));
        }
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Console.WriteLine("--> Getting Platforms by Id....");

            var result = _repository.GetPlatformById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlatformReadDto>(result));
        }
        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform([FromBody] PlatformCreateDto platformCreateDto)
        {
            if (platformCreateDto == null)
            {
                return BadRequest();
            }
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var readDto = _mapper.Map<PlatformReadDto>(platformModel);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendPlatformsToCommand(readDto);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"--> Could not send Synchronously: {ex.Message}");
            }

            // Send Async Message
            try
            {
                var platformPublishedDTO = _mapper.Map<PlatformPublishedDto>(readDto);
                platformPublishedDTO.Event = "Platform_Published";
                _messaegBusClient.PublishNewPlatform(platformPublishedDTO);
            }
            catch (System.Exception ex)
            {

                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = readDto.Id }, readDto);
        }
    }
}