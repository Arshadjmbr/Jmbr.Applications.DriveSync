using DriveSync.DTOS.Request;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IRentalCompanyBl
    {
        Task<string> Register(RegisterCompaniesRequest request);
    }
}
