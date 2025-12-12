using FluentMigrator;

namespace DriveSync.DatabaseLayer.Migrations
{
    [Migration(20251110093002)]
    public class RoleTable : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Execute.Script(@"Scripts\Role.Sql");
        }
    }
}
