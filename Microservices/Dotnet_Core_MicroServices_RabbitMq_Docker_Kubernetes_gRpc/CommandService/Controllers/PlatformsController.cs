using System.Net;
using System.Runtime.InteropServices;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;
        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Get Platforms from CommandService");
            var platformsLst =  _repository.GetAllPlatforms().ToList();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformsLst));
        }


        [HttpPost]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST $ Command Service");
            return Ok("Inbound test from Platforms Controller");
        }
    }
}