using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DatabaseLayer.Dapper.Drivers;
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
using static DriveSync.Enum;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if (!string.IsNullOrEmpty(createDriversRequest.EmailAddress) && !createDriversRequest.EmailAddress.Contains("@"))
            {

                throw new PlatformException((int)HttpStatusCode.Conflict, $"Invalid Email format.");
            }
            Drivers driver = new Drivers
            {
                Name = createDriversRequest.Name,
                Email = createDriversRequest.EmailAddress,
                Age = createDriversRequest.Age,
                StaffIdNum = createDriversRequest.StaffIdNum,
                MobileNumber = createDriversRequest.MobileNumber,
                AlternateMobileNumber = createDriversRequest.AlternateMobileNumber,
                EmiratesId = createDriversRequest.EmiratesId,
                PassportNum = createDriversRequest.PassportNum,
                LicenseTypeId = createDriversRequest.LicenseTypeId,
                LicenseNumber = createDriversRequest.LicenseNumber,
                LicenseExpiryDate = createDriversRequest.LicenseExpiryDate,
                EmiratesZoneId = createDriversRequest.EmiratesZoneId,
                JoiningDate = createDriversRequest.JoiningDate,
                Status = CommonConstants.DRIVER_AVAILABLE,
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

        public async Task<PaginatedResponse<GetDriversResponse>> GetDrivers([FromQuery] FinePaginationRequest request, string? searchText)
        {
            int rowSkip = request.pageSize > 0 ? request.pageSize * request.pageIndex : 0;
            PaginatedResponse<GetDriversResponse> paginatedResponse = new();
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
            var response = (await sqlConnection.QueryAsync<GetDriversResponse>(DriverResource.GetDrivers, parameters)).ToList();
            await sqlConnection.CloseAsync();
            paginatedResponse.Response = response;
            paginatedResponse.TotalRecords = response.FirstOrDefault()?.TotalRecords ?? 0;
            return paginatedResponse;
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
            existingDriver.AlternateMobileNumber = editDriverRequest.AlternateMobileNumber ?? existingDriver.AlternateMobileNumber;
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


        //public async Task<string> BulkUploadDrivers(List<BulkDriverRequest> requests)
        //{
        //    var driversToInsert = new List<Drivers>();
        //    foreach (var item in requests)
        //    {
        //        driversToInsert.Add(new Drivers
        //        { 
        //            Name = item.Name,
        //            Age = item.Age,
        //            StaffIdNum = item.StaffIdNum,
        //            Email = item.EmailAddress,
        //            MobileNumber = item.MobileNumber,
        //            AlternateMobileNumber = item.AlternateMobileNumber,
        //            EmiratesId = item.EmiratesId,
        //            PassportNum = item.PassportNum,
        //            LicenseTypeId = item.LicenseTypeId,
        //            LicenseNumber = item.LicenseNumber,
        //            LicenseExpiryDate = DateOnly.FromDateTime(item.LicenseExpiryDate),
        //            EmiratesZoneId = item.EmiratesZoneId,
        //            JoiningDate = DateOnly.FromDateTime(item.JoiningDate),
        //            Remarks = item.Remarks,
        //            Status = CommonConstants.DRIVER_AVAILABLE,
        //            CreatedDateTime = DateTime.UtcNow,
        //            UpdatedDateTime = DateTime.UtcNow,
        //        });

        //    }
        //    await mDriveSyncDbContext.Driver.AddRangeAsync(driversToInsert);
        //    await mDriveSyncDbContext.SaveChangesAsync();
        //    return $"{driversToInsert.Count} drivers imported successfully.";
        //}



        public async Task<string> BulkUploadDrivers(List<BulkDriverRequest> requests)
        {
            var driversToInsert = new List<Drivers>();
            var validationErrors = new List<string>(); // To store error messages
            int rowTracker = 2; // Excel data usually starts at Row 2

            foreach (var item in requests)
            {
                // --- START VALIDATION ---
                var errors = new List<string>();

                if (string.IsNullOrWhiteSpace(item.Name)) errors.Add("Name is required.");
                if ((item.MobileNumber == 0)) errors.Add("Mobile Number is required.");
                if (string.IsNullOrWhiteSpace(item.StaffIdNum)) errors.Add("StaffId is required");
                if (string.IsNullOrWhiteSpace(item.EmiratesId)) errors.Add("EmiratesId is required");
                if (string.IsNullOrWhiteSpace(item.LicenseNumber)) errors.Add("LicenseNumber is required");
                if (item.LicenseTypeId <= 0) errors.Add("LicenseTypeId will be greater than 0");
                if (item.EmiratesZoneId <= 0) errors.Add("EmiratesZoneId will be greater than 0");
                if (item.JoiningDate > DateTime.Now) errors.Add("Joining Date is not Correct");
                if (item.LicenseExpiryDate < DateTime.Now.AddMonths(-1)) errors.Add("License is already expired.");
                if (item.Age < 18 || item.Age > 65) errors.Add("Age must be between 18 and 65.");

                if (!string.IsNullOrEmpty(item.EmailAddress) && !item.EmailAddress.Contains("@"))
                    errors.Add("Invalid Email format.");

                // If there are errors for this specific row
                if (errors.Any())
                {
                    validationErrors.Add($"Row {rowTracker}: {string.Join(", ", errors)}");
                    rowTracker++;
                    continue;
                }

                driversToInsert.Add(new Drivers
                {
                    Name = item.Name,
                    Age = item.Age,
                    StaffIdNum = item.StaffIdNum,
                    Email = item.EmailAddress,
                    MobileNumber = item.MobileNumber,
                    AlternateMobileNumber = item.AlternateMobileNumber,
                    EmiratesId = item.EmiratesId,
                    PassportNum = item.PassportNum,
                    LicenseTypeId = item.LicenseTypeId,
                    LicenseNumber = item.LicenseNumber,
                    LicenseExpiryDate = DateOnly.FromDateTime(item.LicenseExpiryDate),
                    EmiratesZoneId = item.EmiratesZoneId,
                    JoiningDate = DateOnly.FromDateTime(item.JoiningDate),
                    Remarks = item.Remarks,
                    Status = CommonConstants.DRIVER_AVAILABLE,
                    CreatedDateTime = DateTime.UtcNow,
                    UpdatedDateTime = DateTime.UtcNow,
                });

                rowTracker++;
            }
            if (validationErrors.Any())
            {
                return "Validation Failed: " + string.Join(" | ", validationErrors);
            }

            await mDriveSyncDbContext.Driver.AddRangeAsync(driversToInsert);
            await mDriveSyncDbContext.SaveChangesAsync();
            return $"{driversToInsert.Count} drivers imported successfully.";
        }
    }
}
