using DriveSync.DTOS.Request;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IDashboardBr
    {
        Task<string> AddAssignments(AssignmentRequest request);
    }
}
