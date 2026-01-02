using DriveSync.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace DriveSync.DatabaseLayer.DBContext
{
    public class DriveSyncDbContext:DbContext
    {
        public DriveSyncDbContext(DbContextOptions<DriveSyncDbContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Drivers> Driver { get; set; }
        public DbSet<RentalCompanies> RentalCompanies { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Assignments> Assignments { get; set; }
        public DbSet<Fines> Fines { get; set; }
    }
}
