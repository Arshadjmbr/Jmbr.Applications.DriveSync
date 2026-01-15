using Azure.Core;
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
using Microsoft.AspNetCore.Mvc;
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
                CreatedDateTime = DateTimeOffset.Now,
                UpdatedDateTime = DateTimeOffset.Now,
                Status = CommonConstants.RENTAL_COMPANY_ACTIVE,
                Deleted = 0
            };

            await mDriveSyncDbContext.RentalCompanies.AddAsync(rentalCompanies);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "Company Registered";

        }

        public async Task<PaginatedResponse<RentalCompanyResponse>> GetRentalCompanies([FromQuery] FinePaginationRequest request, string? searchText)
        {
            int rowSkip = request.pageSize > 0 ? request.pageSize * request.pageIndex : 0;
            PaginatedResponse<RentalCompanyResponse> response = new PaginatedResponse<RentalCompanyResponse>();
            using SqlConnection sqlconnection = mSqlService.GetSqlConnection();
              await sqlconnection.OpenAsync();
            var parameters = new
            {
                searchText = string.IsNullOrWhiteSpace(searchText) ? null : searchText,
                fromDate = request.FromDate,
                toDate = request.ToDate,
                rowSkip,
                takeRows = request.pageSize
            };
            var responses = (await sqlconnection.QueryAsync<RentalCompanyResponse>(RentalCompanyResource.GetRentalCompanies, parameters)).ToList();
                await sqlconnection.CloseAsync();
            response.Response = responses;
            response.TotalRecords = responses.FirstOrDefault()?.TotalRecords ?? 0;
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
            existingCompany.UpdatedDateTime = DateTimeOffset.Now;
            mDriveSyncDbContext.RentalCompanies.Update(existingCompany);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "Company updated successfully.";
        }
        public async Task<string> DeleteCompany(long companyId)
        {
            var company = await mDriveSyncDbContext.RentalCompanies.FindAsync(companyId);
            if (company == null)
            {
                throw new PlatformException((int)HttpStatusCode.NotFound, $"Company with ID {companyId} not found.");
            }

            company.Deleted = 1;
            company.UpdatedDateTime = DateTimeOffset.Now;
            mDriveSyncDbContext.RentalCompanies.Update(company);
            await mDriveSyncDbContext.SaveChangesAsync();
            return "Company deleted successfully.";
        }
    }
}
