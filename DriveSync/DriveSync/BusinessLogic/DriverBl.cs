using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

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

        public async Task<List<GetDriversResponse>> GetDrivers()
        {
            return await mDriverBr.GetDrivers();
        }

        public async Task<List<DriversDropdownResponse>> GetDropdowns()
        {
            return await mDriverBr.GetDropdowns();
        }

        public async Task<string> EditDriver(long Id, EditDriverRequest editDriverRequest)
        {
            return await mDriverBr.EditDriver(Id,editDriverRequest);
        }
    }
}
