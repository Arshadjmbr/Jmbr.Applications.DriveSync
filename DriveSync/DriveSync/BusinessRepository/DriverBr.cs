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
using DriveSync.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;
using static DriveSync.Enum;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DriveSync.BusinessRepository
{
    public class DriverBr(DriveSyncDbContext mDriveSyncDbContext, ISqlService mSqlService) : IDriverBr
    {
        public async Task<string> CreateDrivers(CreateDriverRequest createDriversRequest)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            Drivers existingDriver = mDriveSyncDbContext.Driver.Where(u => u.StaffIdNum.Trim().ToLower() == createDriversRequest.StaffIdNum.Trim().ToLower()).FirstOrDefault();
            if (existingDriver != null)
            {
                throw new PlatformException((int)HttpStatusCode.Conflict, $"A driver with {createDriversRequest.StaffIdNum} already exists.");
            }
            if (!string.IsNullOrEmpty(createDriversRequest.EmiratesId))
            {
                if (!RegexUtility.EmiratesIdRegex.IsMatch(createDriversRequest.EmiratesId))
                    throw new PlatformException((int)HttpStatusCode.Conflict, "Invalid Emirates ID format (784-XXXX-XXXXXXX-X).");
            }
            if (!string.IsNullOrEmpty(createDriversRequest.EmailAddress) && !createDriversRequest.EmailAddress.Contains("@"))
                throw new PlatformException((int)HttpStatusCode.Conflict, "Invalid Email format.");


            if (!string.IsNullOrWhiteSpace(createDriversRequest.PassportNum))
            {
                bool passportExists = await mDriveSyncDbContext.Driver.AnyAsync(u =>
                    u.PassportNum.Trim().ToLower() == createDriversRequest.PassportNum.Trim().ToLower());
                if (passportExists)
                    throw new PlatformException((int)HttpStatusCode.Conflict, $"Driver with Passport {createDriversRequest.PassportNum} already exists.");
            }

            if (!string.IsNullOrWhiteSpace(createDriversRequest.EmiratesId))
            {
                bool eidExists = await mDriveSyncDbContext.Driver.AnyAsync(u =>
                    u.EmiratesId.Trim().ToLower() == createDriversRequest.EmiratesId.Trim().ToLower());
                if (eidExists)
                    throw new PlatformException((int)HttpStatusCode.Conflict, $"Driver with Emirates ID {createDriversRequest.EmiratesId} already exists.");
            }

            if (createDriversRequest.MobileNumber != 0)
            {
                bool mobileExists = await mDriveSyncDbContext.Driver.AnyAsync(u => u.MobileNumber == createDriversRequest.MobileNumber);
                if (mobileExists)
                    throw new PlatformException((int)HttpStatusCode.Conflict, $"Mobile Number {createDriversRequest.MobileNumber} is already registered.");
                if(RegexUtility.UaeMobileRegex.IsMatch(createDriversRequest.MobileNumber.ToString()) == false)
                {
                    throw new PlatformException((int)HttpStatusCode.Conflict, "Invalid UAE Mobile Number format.");
                }
            }

            if (createDriversRequest.AlternateMobileNumber != null && createDriversRequest.AlternateMobileNumber != 0)
            {
                Drivers driverWithSameAlternateMobile = mDriveSyncDbContext.Driver
                    .Where(u => u.AlternateMobileNumber == createDriversRequest.AlternateMobileNumber)
                    .FirstOrDefault();
                if (driverWithSameAlternateMobile != null)
                {
                    throw new PlatformException((int)HttpStatusCode.Conflict, $"Another driver with Alternate Mobile Number {createDriversRequest.AlternateMobileNumber} already exists.");
                }
                if(RegexUtility.UaeMobileRegex.IsMatch(createDriversRequest.AlternateMobileNumber.ToString()) == false)
                {
                    throw new PlatformException((int)HttpStatusCode.Conflict, "Invalid UAE Alternate Mobile Number format.");
                }
            }

            if (createDriversRequest.LicenseExpiryDate < today) // Changed from AddMonths(-1) to today for strictness
            {
                throw new PlatformException((int)HttpStatusCode.Conflict, "Cannot add a driver with an expired license.");
            }

            if (createDriversRequest.Age < 18 || createDriversRequest.Age > 65)
            {
                throw new PlatformException((int)HttpStatusCode.Conflict, "Driver age must be between 18 and 65.");
            }

            if (createDriversRequest.JoiningDate > today)
            {
                throw new PlatformException((int)HttpStatusCode.Conflict, "Joining Date cannot be in the future.");
            }

            Drivers driver = new Drivers
            {
                Name = createDriversRequest.Name,
                Email = createDriversRequest.EmailAddress,
                Age = createDriversRequest.Age,
                StaffIdNum = createDriversRequest.StaffIdNum.Trim(),
                MobileNumber = createDriversRequest.MobileNumber,
                AlternateMobileNumber = createDriversRequest.AlternateMobileNumber,
                EmiratesId = createDriversRequest.EmiratesId.Trim(),
                PassportNum = createDriversRequest.PassportNum?.Trim(),
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

            var today = DateOnly.FromDateTime(DateTime.Now);


            if (!string.IsNullOrEmpty(editDriverRequest.EmailAddress) && !editDriverRequest.EmailAddress.Contains("@"))
                throw new PlatformException((int)HttpStatusCode.Conflict, "Invalid Email format.");

            // 2. Duplicate Checks (Refined)
            async Task EnsureUnique(string value, string fieldName, Func<Drivers, string> propertySelector)
            {
                if (string.IsNullOrWhiteSpace(value)) return;
                var duplicate = await mDriveSyncDbContext.Driver.AnyAsync(u =>
                    u.Id != Id && u.PassportNum.ToLower() == value.Trim().ToLower()); // Example for Passport
                                                                                      // Note: Better to use direct DB comparison than propertySelector for SQL translation
            }

            if (!string.IsNullOrEmpty(editDriverRequest.EmiratesId))
            {
                if (!RegexUtility.EmiratesIdRegex.IsMatch(editDriverRequest.EmiratesId))
                {
                    throw new PlatformException((int)HttpStatusCode.Conflict, "Emirates ID must follow the format 784-XXXX-XXXXXXX-X.");
                }
            }

            if (editDriverRequest.MobileNumber != 0)
            {
                bool mobileExists = await mDriveSyncDbContext.Driver.AnyAsync(u => u.MobileNumber == editDriverRequest.MobileNumber && u.Id != Id);
                if (mobileExists)
                    throw new PlatformException((int)HttpStatusCode.Conflict, $"Mobile Number {editDriverRequest.MobileNumber} is already registered.");
                if (RegexUtility.UaeMobileRegex.IsMatch(editDriverRequest.MobileNumber.ToString()) == false)
                {
                    throw new PlatformException((int)HttpStatusCode.Conflict, "Invalid UAE Mobile Number format.");
                }
            }

            if (editDriverRequest.AlternateMobileNumber != null && editDriverRequest.AlternateMobileNumber != 0)
            {
                Drivers driverWithSameAlternateMobile = mDriveSyncDbContext.Driver
                    .Where(u => u.AlternateMobileNumber == editDriverRequest.AlternateMobileNumber && u.Id != Id)
                    .FirstOrDefault();
                if (driverWithSameAlternateMobile != null)
                {
                    throw new PlatformException((int)HttpStatusCode.Conflict, $"Another driver with Alternate Mobile Number {editDriverRequest.AlternateMobileNumber} already exists.");
                }
                if (RegexUtility.UaeMobileRegex.IsMatch(editDriverRequest.AlternateMobileNumber.ToString()) == false)
                {
                    throw new PlatformException((int)HttpStatusCode.Conflict, "Invalid UAE Alternate Mobile Number format.");
                }
            }


            if (editDriverRequest.LicenseExpiryDate.HasValue && editDriverRequest.LicenseExpiryDate < today)
                throw new PlatformException((int)HttpStatusCode.Conflict, "License is already expired.");

            if (editDriverRequest.Age.HasValue && (editDriverRequest.Age < 17 || editDriverRequest.Age > 65))
                throw new PlatformException((int)HttpStatusCode.Conflict, "Age must be between 17 and 65.");

            if (editDriverRequest.JoiningDate.HasValue && editDriverRequest.JoiningDate > today)
                throw new PlatformException((int)HttpStatusCode.Conflict, "Joining Date cannot be in the future.");

            // 4. Vacation & Resignation Logic
            if (editDriverRequest.VacationstartDate.HasValue && editDriverRequest.VacationEndDate.HasValue)
            {
                if (editDriverRequest.VacationEndDate < editDriverRequest.VacationstartDate)
                    throw new PlatformException((int)HttpStatusCode.Conflict, "Vacation End Date cannot be earlier than Start Date.");
            }

            // 5. Update Status based on priority
            if (editDriverRequest.ResignationDate.HasValue)
            {
                if (editDriverRequest.ResignationDate < existingDriver.JoiningDate)
                    throw new PlatformException((int)HttpStatusCode.Conflict, "Resignation Date cannot be before Joining Date.");
                if (editDriverRequest.ResignationDate != null)
                {
                    existingDriver.Status = CommonConstants.DRIVER_RESIGNED;
                    existingDriver.ResignationDate = editDriverRequest.ResignationDate;
                }
            }
            else if (editDriverRequest.VacationstartDate != null)
            {
                //Only set to "On Vacation" if the vacation has actually started
                if (editDriverRequest.VacationstartDate <= today)
                {
                    existingDriver.Status = CommonConstants.DRIVER_ON_VACATION;
                }
                existingDriver.VacationDateFrom = editDriverRequest.VacationstartDate;
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
        public async Task<string> BulkUploadDrivers(List<BulkDriverRequest> requests)
        {
            var driversToInsert = new List<Drivers>();
            var validationErrors = new List<string>(); 
            int rowTracker = 2; 

            foreach (var item in requests)
            {
                var errors = new List<string>();
                var exist = await mDriveSyncDbContext.Driver.FindAsync(item.StaffIdNum);
                if(exist!=null)
                {
                    errors.Add($"Driver with {item.StaffIdNum} already exists");
                    continue;
                }
               


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
        public async Task<DriversCountResponse> GetDriversCount()
        {
            DriversCountResponse driversCountResponse = new DriversCountResponse();
            using (SqlConnection sqlConnection = mSqlService.GetSqlConnection())
            {
                await sqlConnection.OpenAsync();
                var response = await sqlConnection.QueryFirstOrDefaultAsync<DriversCountResponse>(DriverResource.getDriversCount);
                driversCountResponse = response;
                await sqlConnection.CloseAsync();
            }
            return driversCountResponse;
        }
    }
}
