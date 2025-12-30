using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DatabaseLayer.Dapper.RentalCompanies;
using DriveSync.DatabaseLayer.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Entity.Model;
using DriveSync.Handlers.Constants;
using DriveSync.Handlers.ExceptionHandler;
using DriveSync.Services.IServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DriveSync.BusinessRepository
{
    public class RentalCompanyBr(DriveSyncDbContext mDriveSyncDbContext, ISqlService mSqlService) : IRentalCompanyBr
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
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow,
                Status = CommonConstants.RENTAL_COMPANY_ACTIVE,
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

        public async Task<string> UpdateCompany(long id, UpdateCompanyRequest request)
        {
            RentalCompanies existingCompany = mDriveSyncDbContext.RentalCompanies.FirstOrDefault(x => x.Id == id);
            if (existingCompany == null)
            {
                throw new PlatformException((int)HttpStatusCode.NotFound, $"Company with ID {id} not found.");
            }

            existingCompany.CompanyName = request.CompanyName ?? existingCompany.CompanyName;
            existingCompany.Address = request.Address ?? existingCompany.Address;
            existingCompany.ContactNumber = request.ContactNumber ?? existingCompany.ContactNumber;
            existingCompany.Remarks = request.Remarks ?? existingCompany.Remarks;
            existingCompany.UpdatedDateTime = DateTime.UtcNow;
            mDriveSyncDbContext.RentalCompanies.Update(existingCompany);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "Company updated successfully.";
        }
    }
}
