using FluentMigrator;

namespace DriveSync.Migrations
{
    [Migration(20251116094400)]
    public class VehicleTable : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Execute.Script(@"Scripts\Vehicle.sql");
        }
    }
}
