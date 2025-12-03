using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.Entity.Model;

namespace DriveSync.BusinessRepository
{
    public class DashboardBr(DriveSyncDbContext mDriveSyncDbContext):IDashboardBr
    {
        public async Task<string> AddAssignments(AssignmentRequest request)
        {
            Assignments assignments = new Assignments()
            {
                VehicleId = request.VehicleId,
                DriverId = request.DriverId,
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow,
                Remarks = request.Remarks,
                Comments = request.Comments,
                Status = 1
            };
            await mDriveSyncDbContext.Assignments.AddAsync(assignments);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "Assignments Added";
        }
    }
}
