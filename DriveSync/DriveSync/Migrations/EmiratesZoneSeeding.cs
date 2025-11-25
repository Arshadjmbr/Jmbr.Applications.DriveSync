using FluentMigrator;

namespace DriveSync.Migrations
{
    [Migration(20251113060001)]
    public class EmiratesZoneSeeding : Migration
    {
        public override void Down()
        {

        }
        public override void Up()
        {
            Execute.Script(@"Scripts\EmiratesZoneSeeding.sql");
        }
    }
}
