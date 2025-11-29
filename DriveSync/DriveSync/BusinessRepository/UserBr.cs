using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.Dapper.Drivers;
using DriveSync.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
using DriveSync.ExceptionHandler;
using DriveSync.Generator;
using DriveSync.Hashing;
using DriveSync.Services.IServices;
using Microsoft.Data.SqlClient;
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

      
    }
}
