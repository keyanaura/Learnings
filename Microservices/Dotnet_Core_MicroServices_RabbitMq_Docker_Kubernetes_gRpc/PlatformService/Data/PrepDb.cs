using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder? app, bool isProd)
        {
            using (IServiceScope? serviceScope = app?.ApplicationServices.CreateScope())
            {
                AppDbContext? context = serviceScope?.ServiceProvider.GetService<AppDbContext>();
                if (context != null)
                    SeedData(context, isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd )
        {
            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations");
                try
                {
                    context.Database.Migrate();
                }
                catch (System.Exception ex)
                {
                    
                    System.Console.WriteLine($"--> Could not run Migrations: {ex.Message}");
                }
            }
            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data");
                context.Platforms.AddRange(
                    new Platform(){ Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform(){ Name = "Sql Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform(){ Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );
                context.SaveChanges();
            } 
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}