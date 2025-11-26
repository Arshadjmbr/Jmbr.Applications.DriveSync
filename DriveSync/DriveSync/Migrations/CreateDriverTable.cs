using FluentMigrator;

namespace DriveSync.Migrations
{
    [Migration(20251140104501)]
    public class CreateDriverTable : Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Execute.Script(@"Scripts\Driver.sql");
        }
    }
}
