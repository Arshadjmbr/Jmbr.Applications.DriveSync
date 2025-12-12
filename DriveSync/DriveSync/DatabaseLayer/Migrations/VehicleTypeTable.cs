using FluentMigrator;

namespace DriveSync.DatabaseLayer.Migrations
{
    [Migration(20251111140004)]
    public class VehicleTypeTable : Migration
    {
        public override void Down()
        {
            
        }
        public override void Up()
        {
            Execute.Script(@"Scripts\VehicleType.sql");
        }
    }
}
