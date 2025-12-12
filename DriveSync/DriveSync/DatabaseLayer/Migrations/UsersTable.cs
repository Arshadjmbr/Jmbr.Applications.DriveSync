using FluentMigrator;

namespace DriveSync.DatabaseLayer.Migrations
{
    [Migration(20251110104501)]
    public class UsersTable : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Execute.Script(@"Scripts\Users.sql");
        }
    }
}
