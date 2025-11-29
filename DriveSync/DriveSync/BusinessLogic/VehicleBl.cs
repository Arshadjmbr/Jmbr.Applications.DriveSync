using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic
{
    public class VehicleBl(IVehicleBr mVehicleBr): IVehicleBl
    {
        public async Task<List<VehicleTypeResponse>> GetVehicleTypes()
        {
            return await mVehicleBr.GetVehicleTypes();
        }

        public async Task<string> CreateVehicle(CreateVehicleRequest createVehicleRequest)
        {
            return await mVehicleBr.CreateVehicle(createVehicleRequest);
        }

        public async Task<List<GetVehicleResponse>> GetVehicles()
        {
            return await mVehicleBr.GetVehicles();
        }
    }
}
