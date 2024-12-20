
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Synel_staff.Infrastructure.Persistance;

namespace Synel_staff.Infrastructure.Extensions
{
    public static class StartupDbExtension
    {
        public static void CreateDbIfNotExists(this Microsoft.Extensions.Hosting.IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<AppDbContext>>();
            var appContext = services.GetRequiredService<AppDbContext>();

            try
            {
                var databsecrate = appContext.Database.GetService<IDatabaseCreator>()
                   as RelationalDatabaseCreator;
                if (databsecrate != null)
                {
                    logger.LogInformation("enter databsecrate");
                    if (!databsecrate.CanConnect())
                    {
                        databsecrate.Create();
                        logger.LogInformation("enter databsecrate Create");
                    }

                    if (!databsecrate.HasTables())
                    {
                        databsecrate.CreateTables();
                        logger.LogInformation("enter databsecrate CreateTables");
                    }
                    logger.LogInformation("enter databsecrate InitializeDatabase");
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"migration issue {ex.Message}");
            }
        }
    }
}
