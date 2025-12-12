using FluentMigrator;

namespace DriveSync.DatabaseLayer.Migrations
{
    [Migration(20251110102601)]
    public class RoleSeeding : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Execute.Script(@"Scripts\RoleSeeding.sql");
        }
    }
}
