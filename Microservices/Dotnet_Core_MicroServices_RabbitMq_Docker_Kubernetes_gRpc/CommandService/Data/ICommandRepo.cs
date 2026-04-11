using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();
        
        // Plarform Actions.
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform plat);
        bool PlatformExisis(int platformId);
        bool ExternalPlatformExists(int externalPlatformId);

        // Commad Actions.
        IEnumerable<Command> GetCommadsForPlatform(int platformId);
        Command GetCommand(int platformId, int commadId);
        void CreateCommand(int platformId, Command command);
    }
}