using DriveSync.DTOS.Request;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IRentalCompanyBr
    {
        Task<string> Register(RegisterCompaniesRequest request);

    }
}
