using FluentMigrator;

namespace DriveSync.Migrations
{
    [Migration(20251203104501)]
    public class AssignmentsTable : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Execute.Script(@"Scripts\Assignments.sql");
        }
    }
}
