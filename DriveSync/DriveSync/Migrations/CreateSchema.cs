using FluentMigrator;

namespace DriveSync.Migrations
{
    [Migration(20251110092804)]
    public class CreateSchema : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            //Execute.Script(@"Scripts\Schema.sql");
        }
    }
}
