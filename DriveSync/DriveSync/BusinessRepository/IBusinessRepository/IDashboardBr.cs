using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IDashboardBr
    {
        Task<string> AddAssignments(AssignmentRequest request);
        Task<PaginatedResponse<AssignmentsResponse>> GetAllAssignments([FromQuery] AssignmentPaginationRequest request, string? searchText);
        Task<string> UpdateAssignment(long id, UpdateAssignment request);
        Task<string> DeleteAssignment(long id);

    }
}
