using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.Dapper.Dashboard;
using DriveSync.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
using DriveSync.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DriveSync.BusinessRepository
{
    public class DashboardBr(DriveSyncDbContext mDriveSyncDbContext, ISqlService mSqlService):IDashboardBr
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

        public async Task<PaginatedResponse<AssignmentsResponse>> GetAllAssignments([FromQuery] AssignmentPaginationRequest request, string? searchText)
        {
            int rowSkip = request.pageSize > 0 ? request.pageSize * request.pageIndex : 0;
            PaginatedResponse<AssignmentsResponse> paginatedResponse = new();
            await using SqlConnection sqlConnection = mSqlService.GetSqlConnection();
            await sqlConnection.OpenAsync();

            var parameters = new
            {
                searchText = string.IsNullOrWhiteSpace(searchText) ? null : searchText,
                fromDate = request.FromDate,
                toDate = request.ToDate,
                rowSkip,
                takeRows = request.pageSize
            };

            var assignmentsResponses = (await sqlConnection.QueryAsync<AssignmentsResponse>(
        DashboardResource.getAllAssignments,
     parameters
 )).ToList();

            await sqlConnection.CloseAsync();
            paginatedResponse.Response = assignmentsResponses;
            paginatedResponse.TotalRecords = assignmentsResponses.FirstOrDefault()?.TotalRecords ?? 0;

            return paginatedResponse;
        }
    }
}
