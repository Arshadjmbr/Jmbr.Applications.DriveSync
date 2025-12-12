using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DatabaseLayer.Dapper.RentalCompanies;
using DriveSync.DatabaseLayer.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
using DriveSync.Services.IServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DriveSync.BusinessRepository
{
    public class RentalCompanyBr(DriveSyncDbContext mDriveSyncDbContext, ISqlService mSqlService):IRentalCompanyBr
    {
        public async Task<string> Register(RegisterCompaniesRequest request)
        {
            string normalizedName = request.CompanyName?.Trim().ToLowerInvariant();

            bool exists = await mDriveSyncDbContext.RentalCompanies
                .AnyAsync(c => c.CompanyName.Trim().ToLower() == normalizedName
                            && c.Deleted == 0);
            if (exists)
            {
                return "Company Already Exist";
            }

            RentalCompanies rentalCompanies = new RentalCompanies()
            {
                CompanyName = request.CompanyName,
                Address = request.Address,
                ContactNumber = request.ContactNumber,
                Remarks = request.Remarks,
                IsActive = 1,
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow,
                Deleted = 0
            };

            await mDriveSyncDbContext.RentalCompanies.AddAsync(rentalCompanies);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "Company Registered";

        }

        public async Task<List<RentalCompanyResponse>> GetRentalCompanies()
        {
            List<RentalCompanyResponse> response = new List<RentalCompanyResponse>();
            using (SqlConnection sqlconnection = mSqlService.GetSqlConnection())
            {
                await sqlconnection.OpenAsync();
                var responses = await sqlconnection.QueryAsync<RentalCompanyResponse>(RentalCompanyResource.GetRentalCompanies);
                response = responses.ToList();
                await sqlconnection.CloseAsync();
            }
            return response;
        }
    }
}
