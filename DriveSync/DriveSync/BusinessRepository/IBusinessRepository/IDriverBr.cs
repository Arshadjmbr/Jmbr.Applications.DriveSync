using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IDriverBr
    {
        Task<string> CreateDrivers(CreateDriverRequest createDriversRequest);
        Task<List<EmirateZoneResponse>> GetZone();
        Task<List<LicenseTypeResponse>> GetLicenseType();
    }
}
