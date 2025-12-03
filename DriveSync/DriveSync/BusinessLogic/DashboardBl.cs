using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;

namespace DriveSync.BusinessLogic
{
    public class DashboardBl(IDashboardBr mDashboardBr):IDashboardBl
    {
        public async Task<string> AddAssignments(AssignmentRequest request)
        {
            return await mDashboardBr.AddAssignments(request);
        }
    }
}
