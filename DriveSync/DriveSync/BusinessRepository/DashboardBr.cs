using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DatabaseLayer.Dapper.Dashboard;
using DriveSync.DatabaseLayer.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
using DriveSync.Handlers.Constants;
using DriveSync.Handlers.ExceptionHandler;
using DriveSync.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;

namespace DriveSync.BusinessRepository
{
    public class DashboardBr(DriveSyncDbContext mDriveSyncDbContext, ISqlService mSqlService):IDashboardBr
    {
        public async Task<string> AddAssignments(AssignmentRequest request)
        {
            Vehicle vehicle = await mDriveSyncDbContext.Vehicle.FindAsync(request.VehicleId);
            if (vehicle == null)
            {
                throw new PlatformException((int)HttpStatusCode.Conflict,
                    $"Vehicle with Id {request.VehicleId} is not present in the vehicle table.");
            }

            Drivers driver = await mDriveSyncDbContext.Driver.FindAsync(request.DriverId);
            if (driver == null)
            {
                throw new PlatformException((int)HttpStatusCode.Conflict,
                    $"Driver with Id {request.DriverId} is not present in the driver table.");
            }

            using var transaction = await mDriveSyncDbContext.Database.BeginTransactionAsync();

            try
            {
                var assignment = new Assignments
                {
                    VehicleId = request.VehicleId,
                    DriverId = request.DriverId,
                    StartDateTime = request.StartDateTime,
                    EndDateTime = request.EndDateTime,
                    CreatedDateTime = DateTimeOffset.Now,
                    UpdatedDateTime = DateTimeOffset.Now,
                    Remarks = request.Remarks,
                    Comments = request.Comments,
                    Status = CommonConstants.ASSIGNMENT_ASSIGNED
                };

                await mDriveSyncDbContext.Assignments.AddAsync(assignment);

                vehicle.Status = CommonConstants.VEHICLE_ASSIGNED;
                driver.Status = CommonConstants.DRIVER_ASSIGNED;

                await mDriveSyncDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return "Assignment Added";
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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

        public async Task<string> UpdateAssignment(long id, UpdateAssignment request)
        {
            Assignments assignment = await mDriveSyncDbContext.Assignments.FindAsync(id);
            if (assignment == null)
            {
                throw new PlatformException((int)HttpStatusCode.NotFound, $"Assignment with Id {id} not found.");
            }
            var vehicle = await mDriveSyncDbContext.Vehicle.FindAsync(assignment.VehicleId);
            var driver = await mDriveSyncDbContext.Driver.FindAsync(assignment.DriverId);
            if (vehicle == null || driver == null)
            {
                throw new PlatformException((int)HttpStatusCode.Conflict,
                    "Associated Vehicle or Driver not found for the assignment.");
            }
            using var transaction = await mDriveSyncDbContext.Database.BeginTransactionAsync();
            try
            {
                if (request.EndDateTime.HasValue && (request.EndDateTime > assignment.StartDateTime))
                {
                    assignment.EndDateTime = request.EndDateTime;
                    assignment.UpdatedDateTime = DateTime.UtcNow;
                    assignment.Status = CommonConstants.ASSIGNMENT_RETURNED;
                    vehicle.Status = CommonConstants.VEHICLE_AVAILABLE;
                    driver.Status = CommonConstants.DRIVER_AVAILABLE;
                }
                else
                {
                    if (request.EndDateTime.HasValue)
                    {
                        throw new PlatformException((int)HttpStatusCode.BadRequest,
                            "EndDateTime must be greater than StartDateTime.");
                    }
                }
                if (!string.IsNullOrWhiteSpace(request.Remarks))
                {
                    assignment.Remarks = request.Remarks;
                    assignment.UpdatedDateTime = DateTime.UtcNow;
                }
                if (!string.IsNullOrWhiteSpace(request.Comments))
                {
                    assignment.Comments = request.Comments;
                    assignment.UpdatedDateTime = DateTime.UtcNow;
                }
                await mDriveSyncDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return "Assignment Updated Successfully";
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
            
        public async Task<string> DeleteAssignment(long id)
        {
            var assignment = await mDriveSyncDbContext.Assignments.FindAsync(id);
            if (assignment == null)
            {
                throw new PlatformException((int)HttpStatusCode.NotFound, $"Assignment with ID {id} not found.");
            }

            assignment.Deleted = 1;
            assignment.UpdatedDateTime = DateTimeOffset.Now;
            mDriveSyncDbContext.Assignments.Update(assignment);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "Assignment deleted successfully.";
        }
    }
}
