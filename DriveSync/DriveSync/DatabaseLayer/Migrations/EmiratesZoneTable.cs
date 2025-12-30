using FluentMigrator;

namespace DriveSync.DatabaseLayer.Migrations
{
    [Migration(20251111120003)]
    public class EmiratesZoneTable: Migration
    {
        public override void Down()
        {
            
        }
        public override void Up()
        {
            Execute.Script(@"DatabaseLayer\Scripts\EmiratesZones.sql");
        }
    }
}
