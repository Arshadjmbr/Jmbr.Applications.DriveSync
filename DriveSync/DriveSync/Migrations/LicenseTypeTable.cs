using FluentMigrator;

namespace DriveSync.Migrations
{
    [Migration(20251110120000)]
    public class LicenseTypeTable: Migration
    {
        public override void Down()
        {
            
        }
        public override void Up()
        {
            Execute.Script(@"Scripts\LicenseType.sql");
        }

    }
}
