using DriveSync.DTOS.Response;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IVehicleBr
    {
        Task<List<VehicleTypeResponse>> GetVehicleTypes();
    }
}
