using FluentMigrator;

namespace DriveSync.Migrations
{
    [Migration(20251110120004)]
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
