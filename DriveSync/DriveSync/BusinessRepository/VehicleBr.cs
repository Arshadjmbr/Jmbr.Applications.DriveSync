using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DatabaseLayer.Dapper.Vehicle;
using DriveSync.DatabaseLayer.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
using DriveSync.Handlers.Constants;
using DriveSync.Handlers.ExceptionHandler;
using DriveSync.Services.IServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static DriveSync.Enum;

namespace DriveSync.BusinessRepository  
{
    public class VehicleBr(DriveSyncDbContext mDriveSyncDbContext, ISqlService mSqlService):IVehicleBr
    {
        public async Task<List<VehicleTypeResponse>> GetVehicleTypes()
        {
            List<VehicleTypeResponse> vehicleTypes = new List<VehicleTypeResponse>();
            using (SqlConnection sqlConnection = mSqlService.GetSqlConnection())
            {
                await sqlConnection.OpenAsync();
                var vehicleTyperesponses = await sqlConnection.QueryAsync<VehicleTypeResponse>(VehicleResource.GetVehicleTypes);
                vehicleTypes = vehicleTyperesponses.ToList();
                await sqlConnection.CloseAsync();
            }
            return vehicleTypes;
        }

        public async Task<string> CreateVehicle(CreateVehicleRequest request)
        {
            string plateNum = request.PlateNumber?.Trim().ToLowerInvariant();
            bool exists = await mDriveSyncDbContext.Vehicle.AnyAsync(c => c.PlateNumber.Trim().ToLower() == plateNum && c.Deleted ==0);
            if (exists)
            {
                throw new PlatformException((int)HttpStatusCode.BadRequest, $"Vehicle with the Plate Number {request.PlateNumber} already exist!");

            }

            Vehicle vehicle = new Vehicle()
            {
                PlateNumber = request.PlateNumber,
                VehicleTypeId = request.VehicleCategoryId,
                Model = request.Model,
                RentalCompanyId = request.RentalCompanyid,
                Status = CommonConstants.VEHICLE_AVAILABLE,
                MulkiyaExpiryDate = request.MulkiyaExpiryDate,  
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow,
                Remarks = request.Remarks
            };
            await mDriveSyncDbContext.Vehicle.AddAsync(vehicle);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "Vehicle Added Successfully";
        }

        public async Task<List<GetVehicleResponse>> GetVehicles()
        {
            List<GetVehicleResponse> responses = new List<GetVehicleResponse>();
            using (SqlConnection sqlConnection = mSqlService.GetSqlConnection())
            {
                await sqlConnection.OpenAsync();
                var vehicleresponse = await sqlConnection.QueryAsync<GetVehicleResponse>(VehicleResource.GetVehicles);
                responses = vehicleresponse.ToList();
                await sqlConnection.CloseAsync();
            }
            return responses;
        }

        public async Task<List<VehicleDropdownResponse>> GetVehicleDropdowns()
        {
            List<VehicleDropdownResponse> responses = new List<VehicleDropdownResponse>();
            using (SqlConnection sqlConnection = mSqlService.GetSqlConnection())
            {
                await sqlConnection.OpenAsync();
                var vehicleresponse = await sqlConnection.QueryAsync<VehicleDropdownResponse>(VehicleResource.GetDropdownVehicles);
                responses = vehicleresponse.ToList();
                await sqlConnection.CloseAsync();
            }
            return responses;
        }
        public async Task<string> EditVehicle(long id, EditVehicleRequest request)
        {
            string plateNum = request.PlateNumber?.Trim().ToLowerInvariant();
            bool exists = await mDriveSyncDbContext.Vehicle.AnyAsync(c => c.PlateNumber.Trim().ToLower() == plateNum && c.Id != request.Id && c.Deleted == 0);
            if (exists)
            {
                throw new PlatformException((int)HttpStatusCode.BadRequest, $"Vehicle with the Plate Number {request.PlateNumber} already exist!");
            }
            Vehicle vehicle = await mDriveSyncDbContext.Vehicle.FirstOrDefaultAsync(c => c.Id == request.Id && c.Deleted == 0);
            if (vehicle == null)
            {
                throw new PlatformException((int)HttpStatusCode.NotFound, "Vehicle not found");
            }
            vehicle.PlateNumber = request.PlateNumber ?? vehicle.PlateNumber;
            vehicle.VehicleTypeId = request.VehicleTypeId ?? vehicle.VehicleTypeId;
            vehicle.Model = request.Model ?? vehicle.Model;
            vehicle.RentalCompanyId = request.RentalCompanyId ?? vehicle.RentalCompanyId;
            vehicle.MulkiyaExpiryDate = request.MulkiyaExpiryDate ?? vehicle.MulkiyaExpiryDate;
            vehicle.Remarks = request.Remarks ?? vehicle.Remarks;
            vehicle.UpdatedDateTime = DateTime.UtcNow;
            mDriveSyncDbContext.Vehicle.Update(vehicle);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "Vehicle Updated Successfully";
        }
    }
}
