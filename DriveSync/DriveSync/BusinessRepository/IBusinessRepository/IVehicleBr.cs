using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IVehicleBr
    {
        Task<List<VehicleTypeResponse>> GetVehicleTypes();
        Task<string> CreateVehicle(CreateVehicleRequest createVehicleRequest);
        Task<List<GetVehicleResponse>> GetVehicles();
        Task<List<VehicleDropdownResponse>> GetVehicleDropdowns();


    }
}
