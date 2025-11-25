using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.Entity.Model;
using DriveSync.ExceptionHandler;
using DriveSync.Generator;
using DriveSync.Hashing;
using System.Net;
using System.Text.RegularExpressions;
using static DriveSync.Enum;

namespace DriveSync.BusinessRepository
{
    public class UserBr(DriveSyncDbContext mDriveSyncDbContext) : IUserBr
    {
        public async Task<string> CreateUser(CreateUserRequest createUserRequest)
        {
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,}$";
            if (!Regex.IsMatch(createUserRequest.Password, passwordPattern))
            {
                throw new PlatformException((int)HttpStatusCode.BadRequest, "Password must contain at least one lowercase letter, one uppercase letter, and one special character, and be at least 8 characters long.");
            }

            Users? existingUser = mDriveSyncDbContext.Users.Where(u => u.EmailAddress == createUserRequest.EmailAddress).FirstOrDefault();
            if (existingUser != null)
            {
                throw new PlatformException((int)HttpStatusCode.Conflict, "A user with the provided email address already exists.");
            }
            string hashedPassword = PasswordHashing.HashingPassword(createUserRequest.Password);
            //string otp = OtpGenerator.GenerateOTP();
            //DateTime otpExpiry = DateTime.UtcNow.AddMinutes(1);
            //Console.WriteLine(otpExpiry);
            Users user = new Users
            {
                Name = createUserRequest.Name,
                EmailAddress = createUserRequest.EmailAddress,
                Password = hashedPassword,
                Age = createUserRequest.Age ?? 0,
                MobileNumber = createUserRequest.MobileNumber,
                RoleId = createUserRequest.RoleId,
                Status = (int)Status.Active,
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow
            };
            await mDriveSyncDbContext.Users.AddAsync(user);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "User created successfully.";
        }

        public async Task<string> CreateDrivers(CreateDriverRequest createDriversRequest)
        {
            Users? existingUser = mDriveSyncDbContext.Users.Where(u => u.EmailAddress == createDriversRequest.EmailAddress).FirstOrDefault();
            if (existingUser != null)
            {
                throw new PlatformException((int)HttpStatusCode.Conflict, $"A user with the email address {createDriversRequest.EmailAddress} already exists.");
            }
            Drivers driver = new Drivers
            {
                Name = createDriversRequest.Name,
                EmailAddress = createDriversRequest.EmailAddress,
                Age = createDriversRequest.Age,
                StaffIdNum = createDriversRequest.StaffIdNum ?? "",
                MobileNumber = createDriversRequest.MobileNumber,
                EmiratesId = createDriversRequest.EmiratesId,
                PassportNum = createDriversRequest.PassportNum,
                LicenseType = createDriversRequest.LicenseType,
                LicenseNumber = createDriversRequest.LicenseNumber,
                LicenseExpiryDate = createDriversRequest.LicenseExpiryDate,
                EmiratesZone = createDriversRequest.EmiratesZone,
                JoiningDate = createDriversRequest.JoiningDate,
                IsActive = (int)Status.Active,
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow,
                Remarks = createDriversRequest.Remarks,
                Deleted = 0
            };
            await mDriveSyncDbContext.Drivers.AddAsync(driver);

            await mDriveSyncDbContext.SaveChangesAsync();
            return "Drivers created successfully.";
        }
    }
}
