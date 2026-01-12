using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IDriverBr
    {
        Task<string> CreateDrivers(CreateDriverRequest createDriversRequest);
        Task<List<EmirateZoneResponse>> GetZone();
        Task<List<LicenseTypeResponse>> GetLicenseType();
        Task<PaginatedResponse<GetDriversResponse>> GetDrivers([FromQuery] FinePaginationRequest request, string? searchText);
        Task<List<DriversDropdownResponse>> GetDropdowns();
        Task<string> EditDriver(long Id, EditDriverRequest editDriverRequest);
        Task<string> BulkUploadDrivers(List<BulkDriverRequest> driverList);
        Task<DriversCountResponse> GetDriversCount();

    }
}
