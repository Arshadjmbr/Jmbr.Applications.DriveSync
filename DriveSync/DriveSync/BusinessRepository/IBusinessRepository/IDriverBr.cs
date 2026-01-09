using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IDriverBr
    {
        Task<string> CreateDrivers(CreateDriverRequest createDriversRequest);
        Task<List<EmirateZoneResponse>> GetZone();
        Task<List<LicenseTypeResponse>> GetLicenseType();
        Task<List<GetDriversResponse>> GetDrivers();
        Task<List<DriversDropdownResponse>> GetDropdowns();
        Task<string> EditDriver(long Id, EditDriverRequest editDriverRequest);
        Task<string> BulkUploadDrivers(List<BulkDriverRequest> driverList);

    }
}
