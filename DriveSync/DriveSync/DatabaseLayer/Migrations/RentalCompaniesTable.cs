using FluentMigrator;

namespace DriveSync.DatabaseLayer.Migrations
{
    [Migration(20251115094400)]
    public class RentalCompaniesTable : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Execute.Script(@"Scripts\RentalCompanies.sql");
        }
    }
}
