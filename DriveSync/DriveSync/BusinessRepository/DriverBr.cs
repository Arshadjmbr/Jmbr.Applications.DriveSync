using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.Dapper.Drivers;
using DriveSync.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
using DriveSync.ExceptionHandler;
using DriveSync.Services.IServices;
using Microsoft.Data.SqlClient;
using System.Net;
using static DriveSync.Enum;

namespace DriveSync.BusinessRepository
{
    public class DriverBr(DriveSyncDbContext mDriveSyncDbContext, ISqlService mSqlService):IDriverBr
    {
        public async Task<string> CreateDrivers(CreateDriverRequest createDriversRequest)
        {
            Drivers existingDriver = mDriveSyncDbContext.Driver.Where(u => u.PassportNum.Trim().ToLower() == createDriversRequest.PassportNum.Trim().ToLower()).FirstOrDefault();
            if (existingDriver != null)
            {
                throw new PlatformException((int)HttpStatusCode.Conflict, $"A driver with {createDriversRequest.PassportNum} already exists.");
            }
            Drivers driver = new Drivers
            {
                Name = createDriversRequest.Name,
                Email = createDriversRequest.EmailAddress,
                Age = createDriversRequest.Age,
                StaffIdNum = createDriversRequest.StaffIdNum ?? "",
                MobileNumber = createDriversRequest.MobileNumber,
                EmiratesId = createDriversRequest.EmiratesId,
                PassportNum = createDriversRequest.PassportNum,
                LicenseTypeId = createDriversRequest.LicenseTypeId,
                LicenseNumber = createDriversRequest.LicenseNumber,
                LicenseExpiryDate = createDriversRequest.LicenseExpiryDate,
                EmiratesZoneId = createDriversRequest.EmiratesZoneId,
                JoiningDate = createDriversRequest.JoiningDate,
                IsActive = (int)Status.Active,
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow,
                Remarks = createDriversRequest.Remarks
            };
            await mDriveSyncDbContext.Driver.AddAsync(driver);

            await mDriveSyncDbContext.SaveChangesAsync();
            return "Driver created successfully.";
        }

        public async Task<List<EmirateZoneResponse>> GetZone()
        {
            List<EmirateZoneResponse> zones = new List<EmirateZoneResponse>();
            using (SqlConnection sqlConnection = mSqlService.GetSqlConnection())
            {
                await sqlConnection.OpenAsync();
                var zoneresponses = await sqlConnection.QueryAsync<EmirateZoneResponse>(DriverResource.GetEmirateZone);
                zones = zoneresponses.ToList();
                await sqlConnection.CloseAsync();
            }
            return zones;
        }
        public async Task<List<LicenseTypeResponse>> GetLicenseType()
        {
            List<LicenseTypeResponse> licenseTypes = new List<LicenseTypeResponse>();
            using (SqlConnection sqlConnection = mSqlService.GetSqlConnection())
            {
                await sqlConnection.OpenAsync();
                var licenseTyperesponses = await sqlConnection.QueryAsync<LicenseTypeResponse>(DriverResource.GetLicenseTypes);
                licenseTypes = licenseTyperesponses.ToList();
                await sqlConnection.CloseAsync();
            }
            return licenseTypes;
        }

        public async Task<List<GetDriversResponse>> GetDrivers()
        {
            List<GetDriversResponse> getDriversResponses = new List<GetDriversResponse>();
            using (SqlConnection sqlConnection = mSqlService.GetSqlConnection())
            {
                await sqlConnection.OpenAsync();
                var response = await sqlConnection.QueryAsync<GetDriversResponse>(DriverResource.GetDrivers);
                getDriversResponses = response.ToList();
                await sqlConnection.CloseAsync();
            }
            return getDriversResponses;
        }
    }
}
