using Microsoft.EntityFrameworkCore;

namespace DriveSync.DBContext
{
    public class DriveSyncDbContext:DbContext
    {
        public DriveSyncDbContext(DbContextOptions<DriveSyncDbContext> options) : base(options)
        {

        }
    }
}
