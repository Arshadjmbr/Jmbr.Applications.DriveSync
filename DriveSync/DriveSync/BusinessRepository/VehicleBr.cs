using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DatabaseLayer.Dapper.Vehicle;
using DriveSync.DatabaseLayer.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
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
                VehicleTypeId = request.VehicleTypeId,
                Model = request.Model,
                Category = request.Category,
                RentalCompanyid = request.RentalCompanyid,
                IsActive = (int)Status.Active,
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
    }
}
