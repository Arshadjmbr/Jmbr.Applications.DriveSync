using DriveSync.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace DriveSync.DBContext
{
    public class DriveSyncDbContext:DbContext
    {
        public DriveSyncDbContext(DbContextOptions<DriveSyncDbContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
    }
}
