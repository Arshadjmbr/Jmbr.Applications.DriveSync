using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IRentalCompanyBl
    {
        Task<string> Register(RegisterCompaniesRequest request);
        Task<List<RentalCompanyResponse>> GetRentalCompanies();
        Task<string> UpdateCompany(long companyId, UpdateCompanyRequest updateCompanyRequest);
    }
}
