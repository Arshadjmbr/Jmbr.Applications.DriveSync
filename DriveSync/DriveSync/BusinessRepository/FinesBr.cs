using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DatabaseLayer.Dapper.Drivers;
using DriveSync.DatabaseLayer.Dapper.Fines;
using DriveSync.DatabaseLayer.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
using DriveSync.Handlers.ExceptionHandler;
using DriveSync.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DriveSync.BusinessRepository
{
    public class FinesBr(DriveSyncDbContext mDrivesSyncDbContext, ISqlService mSqlService) : IFinesBr
    {
        public async Task<string> AddFine(AddFineRequest addFineRequest)
        {

            var existingVehicle = await mDrivesSyncDbContext.Vehicle.FindAsync(addFineRequest.VehicleId);
            if (existingVehicle == null)
            {
                throw new PlatformException((int)HttpStatusCode.NotFound, "Vehicle Not found");
            }
            var driverId = await mDrivesSyncDbContext.Assignments
                .Where(a => a.VehicleId == addFineRequest.VehicleId &&
                addFineRequest.IssuedDate >= a.StartDateTime &&
                addFineRequest.IssuedDate <= a.EndDateTime)
                .Select(a => a.DriverId)
                .FirstOrDefaultAsync();
            if (driverId == 0)
            {
                throw new PlatformException((int)HttpStatusCode.NotFound, "No driver assigned to this vehicle on the issued date.");
            }

            //decimal existingFines = await mDrivesSyncDbContext.Fines
            //    .Where(f => f.DriverId == driverId && f.Paid == 0)
            //    .SumAsync(f => f.Amount);

            Fines fine = new Fines
            {
                DriverId = driverId,
                VehicleId = addFineRequest.VehicleId,
                ViolationType = addFineRequest.ViolationType,
                ReceiptNumber = addFineRequest.ReceiptNumber,
                FineNumber = addFineRequest.FineNumber,
                Amount = addFineRequest.Amount,
                Reason = addFineRequest.Reason,
                IssuedDate = addFineRequest.IssuedDate,
                CreatedDateTime = DateTimeOffset.Now,
                UpdatedDateTime = DateTimeOffset.Now
            };
            await mDrivesSyncDbContext.Fines.AddAsync(fine);
            await mDrivesSyncDbContext.SaveChangesAsync();
            return "Fine added successfully.";
        }

        public async Task<string> UpdateFine(long fineId, UpdateFineRequest updateFineRequest)
        {
            var fine = await mDrivesSyncDbContext.Fines.FindAsync(fineId);
            if (fine == null)
            {
                throw new PlatformException((int)HttpStatusCode.NotFound, $"Fine with ID {fineId} not found.");
            }

            var effectiveVehicleId = updateFineRequest.VehicleId ?? fine.VehicleId;
            var effectiveIssuedDate = updateFineRequest.IssuedDate ?? fine.IssuedDate;

            if (updateFineRequest.VehicleId.HasValue || updateFineRequest.IssuedDate.HasValue)
            {
                var newDriverId = await mDrivesSyncDbContext.Assignments
                    .Where(a => a.VehicleId == effectiveVehicleId &&
                                effectiveIssuedDate >= a.StartDateTime &&
                                (a.EndDateTime == null || effectiveIssuedDate <= a.EndDateTime))
                    .Select(a => a.DriverId)
                    .FirstOrDefaultAsync();

                if (newDriverId != updateFineRequest.DriverId)
                {
                    throw new PlatformException((int)HttpStatusCode.NotFound, "No driver assigned to this vehicle on the issued date."); 
                }

                fine.DriverId = newDriverId;
            }


            //decimal existingFines = await mDrivesSyncDbContext.Fines
            //    .Where(f => f.DriverId == fine.DriverId &&
            //                f.Paid == 0 &&
            //                f.Id != fineId)
            //    .SumAsync(f => f.Amount);

            //decimal oldFineAmount = fine.Amount;

            //existingFines -= oldFineAmount;

                if (updateFineRequest.VehicleId.HasValue)
                {
                    fine.VehicleId = updateFineRequest.VehicleId.Value;
                }
                if (!string.IsNullOrEmpty(updateFineRequest.ViolationType))
                {
                    fine.ViolationType = updateFineRequest.ViolationType;
                }
                if (!string.IsNullOrEmpty(updateFineRequest.ReceiptNumber))
                {
                    fine.ReceiptNumber = updateFineRequest.ReceiptNumber;
                }
                if (!string.IsNullOrEmpty(updateFineRequest.FineNumber))
                {
                    fine.FineNumber = updateFineRequest.FineNumber;
                }
                if (updateFineRequest.Amount.HasValue)
                {
                    fine.Amount = updateFineRequest.Amount.Value;
                }
                if (!string.IsNullOrEmpty(updateFineRequest.Reason))
                {
                    fine.Reason = updateFineRequest.Reason;
                }
                if (updateFineRequest.IssuedDate.HasValue)
                {
                    fine.IssuedDate = updateFineRequest.IssuedDate.Value;
                }
                if (updateFineRequest.Paid.HasValue)
                {
                    fine.Paid = updateFineRequest.Paid.Value;
                }
                if (updateFineRequest.PaidDate.HasValue)
                {
                    fine.PaidDate = updateFineRequest.PaidDate.Value;
                }
                fine.UpdatedDateTime = DateTimeOffset.Now;

            await mDrivesSyncDbContext.SaveChangesAsync();
            return "Fine updated successfully.";
        }

        public async Task<PaginatedResponse<GetFineResponse>> GetFines([FromQuery] FinePaginationRequest request, string? searchText)
        {
            int rowSkip = request.pageSize > 0 ? request.pageSize * request.pageIndex : 0;
            PaginatedResponse<GetFineResponse> paginatedResponse = new PaginatedResponse<GetFineResponse>();
            using SqlConnection sqlConnection = mSqlService.GetSqlConnection();
            await sqlConnection.OpenAsync();
            var parameters = new
            {
                searchText = string.IsNullOrWhiteSpace(searchText) ? null : searchText,
                fromDate = request.FromDate,
                toDate = request.ToDate,
                rowSkip,
                takeRows = request.pageSize
            };
                var fineResposnes = (await sqlConnection.QueryAsync<GetFineResponse>(FineResource.getFines, parameters)).ToList();
            await sqlConnection.CloseAsync();
            paginatedResponse.Response = fineResposnes;
                paginatedResponse.TotalRecords =fineResposnes.FirstOrDefault()?.TotalRecords ?? 0;

            return paginatedResponse;
        }

        public async Task<List<VehicleDropdownResponse>> GetVehicleDropdown()
        {
            List<VehicleDropdownResponse> zones = new List<VehicleDropdownResponse>();
            using (SqlConnection sqlConnection = mSqlService.GetSqlConnection())
            {
                await sqlConnection.OpenAsync();
                var zoneresponses = await sqlConnection.QueryAsync<VehicleDropdownResponse>(FineResource.getvehicles_fines);
                zones = zoneresponses.ToList();
                await sqlConnection.CloseAsync();
            }
            return zones;
        }
    }
}
