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
        Task<List<DriversDropdownResponse>> GetDropdowns();
        Task<string> EditDriver(long Id,EditDriverRequest editDriverRequest);
        Task<string> BulkUploadDrivers(List<BulkDriverRequest> driverList);

    }
}
