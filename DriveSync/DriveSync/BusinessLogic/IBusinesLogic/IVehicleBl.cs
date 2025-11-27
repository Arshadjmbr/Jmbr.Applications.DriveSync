using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IVehicleBl
    {
        Task<List<VehicleTypeResponse>> GetVehicleTypes();
    }
}
