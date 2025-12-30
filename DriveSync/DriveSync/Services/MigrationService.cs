using FluentMigrator.Runner;

namespace DriveSync.Services
{
    public static class MigrationService
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IMigrationRunner migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                try
                {
                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                }



                catch
                {
                    //log errors or ...
                    throw;
                }
            }
            return host;
        }
    }
}
