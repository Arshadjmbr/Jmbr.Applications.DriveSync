using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IVehicleBl
    {
        Task<List<VehicleTypeResponse>> GetVehicleTypes();
        Task<string> CreateVehicle(CreateVehicleRequest createVehicleRequest);
        Task<PaginatedResponse<GetVehicleResponse>> GetVehicles([FromQuery] FinePaginationRequest request, string? searchText);
        Task<List<VehicleDropdownResponse>> GetVehicleDropdowns();
        Task<string> EditVehicle(long id, EditVehicleRequest editVehicleRequest);

        Task<VehicleCountResponse> GetVehicleCount();
        Task<string> DeleteVehicle(long id);
    }
}
