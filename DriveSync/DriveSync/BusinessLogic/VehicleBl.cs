using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic
{
    public class VehicleBl(IVehicleBr mVehicleBr): IVehicleBl
    {
        public async Task<List<VehicleTypeResponse>> GetVehicleTypes()
        {
            return await mVehicleBr.GetVehicleTypes();
        }
    }
}
