using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IVehicleBl
    {
        Task<List<VehicleTypeResponse>> GetVehicleTypes();
        Task<string> CreateVehicle(CreateVehicleRequest createVehicleRequest);

        Task<List<GetVehicleResponse>> GetVehicles();
    }
}
