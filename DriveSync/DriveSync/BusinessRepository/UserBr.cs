using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DatabaseLayer.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
using DriveSync.Handlers.Constants;
using DriveSync.Handlers.ExceptionHandler;
using DriveSync.Handlers.Hashing;
using DriveSync.Services.IServices;
using DriveSync.Utility;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;
using static DriveSync.Enum;

namespace DriveSync.BusinessRepository
{
    public class UserBr(DriveSyncDbContext mDriveSyncDbContext, ISqlService mSqlService) : IUserBr
    {
        public async Task<string> CreateUser(CreateUserRequest createUserRequest)
        {
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,}$";
            if (!Regex.IsMatch(createUserRequest.Password, passwordPattern))
            {
                throw new PlatformException((int)HttpStatusCode.BadRequest, "Password must contain at least one lowercase letter, one uppercase letter, and one special character, and be at least 8 characters long.");
            }

            Users? existingUser = mDriveSyncDbContext.Users.Where(u => u.StaffId == createUserRequest.StaffId).FirstOrDefault();
            if (existingUser != null)
            {
                throw new PlatformException((int)HttpStatusCode.Conflict, "A user with the provided email address already exists.");
            }

            if (!string.IsNullOrEmpty(createUserRequest.EmailAddress) && !createUserRequest.EmailAddress.Contains("@"))
                throw new PlatformException((int)HttpStatusCode.Conflict, "Invalid Email format.");
            if (createUserRequest.MobileNumber != 0)
            {
                bool mobileExists = await mDriveSyncDbContext.Users.AnyAsync(u => u.MobileNumber == createUserRequest.MobileNumber);
                if (mobileExists)
                    throw new PlatformException((int)HttpStatusCode.Conflict, $"Mobile Number {createUserRequest.MobileNumber} is already registered.");
                if (RegexUtility.UaeMobileRegex.IsMatch(createUserRequest.MobileNumber.ToString()) == false)
                {
                    throw new PlatformException((int)HttpStatusCode.Conflict, "Invalid UAE Mobile Number format.");
                }
            }
            if (createUserRequest.AlternateMobileNumber != null && createUserRequest.AlternateMobileNumber != 0)
            {
                Users driverWithSameAlternateMobile = mDriveSyncDbContext.Users
                    .Where(u => u.AlternateMobileNumber == createUserRequest.AlternateMobileNumber)
                    .FirstOrDefault();
                if (driverWithSameAlternateMobile != null)
                {
                    throw new PlatformException((int)HttpStatusCode.Conflict, $"Another User with Alternate Mobile Number {createUserRequest.AlternateMobileNumber} already exists.");
                }
                if (RegexUtility.UaeMobileRegex.IsMatch(createUserRequest.AlternateMobileNumber.ToString()) == false)
                {
                    throw new PlatformException((int)HttpStatusCode.Conflict, "Invalid UAE Alternate Mobile Number format.");
                }
            }
            string hashedPassword = PasswordHashing.HashingPassword(createUserRequest.Password);
            //string otp = OtpGenerator.GenerateOTP();
            //DateTime otpExpiry = DateTime.UtcNow.AddMinutes(1);
            //Console.WriteLine(otpExpiry);

            Users user = new Users
            {
                Name = createUserRequest.Name,
                EmailAddress = createUserRequest.EmailAddress,
                StaffId = createUserRequest.StaffId,
                Password = hashedPassword,
                Age = createUserRequest.Age ?? 0,
                MobileNumber = createUserRequest.MobileNumber,
                AlternateMobileNumber = createUserRequest.AlternateMobileNumber,
                RoleId = createUserRequest.RoleId,
                Status = CommonConstants.USER_STATUS_ACTIVE,
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow
            };
            await mDriveSyncDbContext.Users.AddAsync(user);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "User created successfully.";
        }

      
    }
}
