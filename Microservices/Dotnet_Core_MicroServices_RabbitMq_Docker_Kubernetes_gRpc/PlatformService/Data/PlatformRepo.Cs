using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;
        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePlatform(Platform platForm)
        {
           if(platForm == null)
            {
                throw new ArgumentNullException(nameof(platForm));
            }
            _context.Add(platForm);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return  _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(x=>x.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >=0);
        }
    }
}