using FluentMigrator;

namespace DriveSync.DatabaseLayer.Migrations
{
    [Migration(20260101120000)]
    public class FinesTable : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Execute.Script(@"DatabaseLayer\Scripts\Fines.sql");
        }
    }
}
