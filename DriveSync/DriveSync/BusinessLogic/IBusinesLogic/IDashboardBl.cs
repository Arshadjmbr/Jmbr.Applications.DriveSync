using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IDashboardBl
    {
        Task<string> AddAssignments(AssignmentRequest request);
        Task<PaginatedResponse<AssignmentsResponse>> GetAllAssignments([FromQuery] AssignmentPaginationRequest request, string? searchText);
        Task<string> UpdateAssignment(long id, UpdateAssignment request);
        Task<string> DeleteAssignment(long id);
    }
}
