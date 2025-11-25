using FluentMigrator;

namespace DriveSync.Migrations
{
    [Migration(20251110120003)]
    public class EmiratesZoneTable: Migration
    {
        public override void Down()
        {
            
        }
        public override void Up()
        {
            Execute.Script(@"Scripts\EmiratesZones.sql");
        }
    }
}
