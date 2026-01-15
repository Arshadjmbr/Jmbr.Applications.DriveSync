using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IRentalCompanyBr
    {
        Task<string> Register(RegisterCompaniesRequest request);
        Task<PaginatedResponse<RentalCompanyResponse>> GetRentalCompanies([FromQuery] FinePaginationRequest request, string? searchText);
        Task<string> UpdateCompany(long companyId, UpdateCompanyRequest updateCompanyRequest);
        Task<string> DeleteCompany(long companyId);

    }
}
