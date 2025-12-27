using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DatabaseLayer.Dapper.Drivers;
using DriveSync.DatabaseLayer.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
using DriveSync.Handlers.ExceptionHandler;
using DriveSync.Services.IServices;
using Microsoft.Data.SqlClient;
using System.Net;
using static DriveSync.Enum;

namespace DriveSync.BusinessRepository
{
    public class DriverBr(DriveSyncDbContext mDriveSyncDbContext, ISqlService mSqlService) : IDriverBr
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

        public async Task<List<DriversDropdownResponse>> GetDropdowns()
        {
            List<DriversDropdownResponse> getDriversResponses = new List<DriversDropdownResponse>();
            using (SqlConnection sqlConnection = mSqlService.GetSqlConnection())
            {
                await sqlConnection.OpenAsync();
                var response = await sqlConnection.QueryAsync<DriversDropdownResponse>(DriverResource.GetDropdownDrivers);
                getDriversResponses = response.ToList();
                await sqlConnection.CloseAsync();
            }
            return getDriversResponses;
        }

        public async Task<string> EditDriver(long Id, EditDriverRequest editDriverRequest)
        {
            Drivers existingDriver = mDriveSyncDbContext.Driver.Where(u => u.Id == Id).FirstOrDefault();
            if (existingDriver == null)
            {
                throw new PlatformException((int)HttpStatusCode.NotFound, $"Driver with ID {Id} not found.");
            }
            existingDriver.Name = editDriverRequest.Name ?? existingDriver.Name;
            existingDriver.Email = editDriverRequest.EmailAddress ?? existingDriver.Email;
            existingDriver.Age = editDriverRequest.Age ?? existingDriver.Age;
            existingDriver.StaffIdNum = editDriverRequest.StaffIdNum ?? existingDriver.StaffIdNum;
            existingDriver.MobileNumber = editDriverRequest.MobileNumber ?? existingDriver.MobileNumber;
            existingDriver.EmiratesId = editDriverRequest.EmiratesId ?? existingDriver.EmiratesId;
            existingDriver.PassportNum = editDriverRequest.PassportNum ?? existingDriver.EmiratesId;
            existingDriver.LicenseTypeId = editDriverRequest.LicenseTypeId ?? existingDriver.LicenseTypeId;
            existingDriver.LicenseNumber = editDriverRequest.LicenseNumber ?? existingDriver.LicenseNumber;
            existingDriver.LicenseExpiryDate = editDriverRequest.LicenseExpiryDate ?? existingDriver.LicenseExpiryDate;
            existingDriver.EmiratesZoneId = editDriverRequest.EmiratesZoneId ?? existingDriver.EmiratesZoneId;
            existingDriver.JoiningDate = editDriverRequest.JoiningDate ?? existingDriver.JoiningDate;
            existingDriver.UpdatedDateTime = DateTime.UtcNow;
            existingDriver.Remarks = editDriverRequest.Remarks ?? existingDriver.Remarks;
            mDriveSyncDbContext.Driver.Update(existingDriver);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "Successfully updated driver.";
        }
    }
}
