using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace DriveSync.BusinessLogic
{
    public class DashboardBl(IDashboardBr mDashboardBr):IDashboardBl
    {
        public async Task<string> AddAssignments(AssignmentRequest request)
        {
            return await mDashboardBr.AddAssignments(request);
        }

        public async Task<PaginatedResponse<AssignmentsResponse>> GetAllAssignments([FromQuery] AssignmentPaginationRequest request, string? searchText)
        {
            return await mDashboardBr.GetAllAssignments(request, searchText);
        }

        public async Task<string> UpdateAssignment(long id, UpdateAssignment request)
        {
            return await mDashboardBr.UpdateAssignment(id, request);
        }
        public async Task<string> DeleteAssignment(long id)
        {
            return await mDashboardBr.DeleteAssignment(id);
        }
    }
}
