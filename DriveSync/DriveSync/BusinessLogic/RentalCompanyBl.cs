using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessLogic
{
    public class RentalCompanyBl(IRentalCompanyBr mRentalCompanyBr):IRentalCompanyBl
    {
        public async Task<string> Register(RegisterCompaniesRequest request)
        {
            return await mRentalCompanyBr.Register(request);
        }

        public async Task<PaginatedResponse<RentalCompanyResponse>> GetRentalCompanies([FromQuery] FinePaginationRequest request, string? searchText)
        {
            return await mRentalCompanyBr.GetRentalCompanies(request, searchText);
        }

        public async Task<string> UpdateCompany(long id, UpdateCompanyRequest updateCompanyRequest)
        {
            return await mRentalCompanyBr.UpdateCompany(id, updateCompanyRequest);
        }
        public async Task<string> DeleteCompany(long companyId)
        {
            return await mRentalCompanyBr.DeleteCompany(companyId);
        }
    }
}
