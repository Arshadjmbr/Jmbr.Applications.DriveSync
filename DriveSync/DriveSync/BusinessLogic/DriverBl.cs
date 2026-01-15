using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessLogic
{
    public class DriverBl(IDriverBr mDriverBr) : IDriverBl
    {
        public async Task<string> CreateDrivers(CreateDriverRequest createDriversRequest)
        {
            return await mDriverBr.CreateDrivers(createDriversRequest);
        }

        public async Task<List<EmirateZoneResponse>> GetZone()
        {
            return await mDriverBr.GetZone();
        }
        public async Task<List<LicenseTypeResponse>> GetLicenseType()
        {
            return await mDriverBr.GetLicenseType();
        }

        public async Task<PaginatedResponse<GetDriversResponse>> GetDrivers([FromQuery] FinePaginationRequest request, string? searchText)
        {
            return await mDriverBr.GetDrivers(request, searchText);
        }

        public async Task<List<DriversDropdownResponse>> GetDropdowns()
        {
            return await mDriverBr.GetDropdowns();
        }

        public async Task<string> EditDriver(long Id, EditDriverRequest editDriverRequest)
        {
            return await mDriverBr.EditDriver(Id,editDriverRequest);
        }

        public async Task<string> BulkUploadDrivers(List<BulkDriverRequest> driversList)
        {
            return await mDriverBr.BulkUploadDrivers(driversList);
        }
        public async Task<DriversCountResponse> GetDriversCount()
        {
            return await mDriverBr.GetDriversCount();
        }
        public async Task<string> DeleteDriver(long id)
        {
            return await mDriverBr.DeleteDriver(id);
        }
    }
}
