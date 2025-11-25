using FluentMigrator;

namespace DriveSync.Migrations
{
    [Migration(20251113070006)]
    public class LicenseTypeSeeding : Migration
    {
        public override void Down()
        {

        }
        public override void Up()
        {
            Execute.Script(@"Scripts\LicenseTypeSeeding.sql");
        }
    }
}
