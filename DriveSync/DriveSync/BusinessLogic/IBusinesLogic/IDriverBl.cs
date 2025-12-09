using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IDriverBl
    {
        Task<string> CreateDrivers(CreateDriverRequest createDriversRequest);
        Task<List<EmirateZoneResponse>> GetZone();
        Task<List<LicenseTypeResponse>> GetLicenseType();
        Task<List<GetDriversResponse>> GetDrivers();
        Task<List<GetDropdownResponse>> GetDropdowns();

    }
}
