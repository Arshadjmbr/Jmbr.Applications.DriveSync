using FluentMigrator;

namespace DriveSync.Migrations
{
    [Migration(20251113082003)]
    public class VehicleTypeSeeding : Migration
    {
        public override void Down()
        {

        }
        public override void Up()
        {
            Execute.Script(@"Scripts\VehicleTypeSeeding.sql");
        }
    }
}
