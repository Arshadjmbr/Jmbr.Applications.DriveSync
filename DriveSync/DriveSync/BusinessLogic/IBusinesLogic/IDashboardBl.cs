using DriveSync.DTOS.Request;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IDashboardBl
    {
        Task<string> AddAssignments(AssignmentRequest request);
    }
}
