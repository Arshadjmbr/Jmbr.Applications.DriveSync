using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IRentalCompanyBr
    {
        Task<string> Register(RegisterCompaniesRequest request);
        Task<List<RentalCompanyResponse>> GetRentalCompanies();

    }
}
