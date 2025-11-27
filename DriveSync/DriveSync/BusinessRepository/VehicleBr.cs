using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.Dapper.Vehicle;
using DriveSync.DBContext;
using DriveSync.DTOS.Response;
using DriveSync.Services.IServices;
using Microsoft.Data.SqlClient;

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
    }
}
