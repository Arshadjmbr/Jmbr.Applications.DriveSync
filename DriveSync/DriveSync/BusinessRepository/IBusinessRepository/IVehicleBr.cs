using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IVehicleBr
    {
        Task<List<VehicleTypeResponse>> GetVehicleTypes();
        Task<string> CreateVehicle(CreateVehicleRequest createVehicleRequest);
        Task<PaginatedResponse<GetVehicleResponse>> GetVehicles([FromQuery] FinePaginationRequest request, string? searchText);
        Task<List<VehicleDropdownResponse>> GetVehicleDropdowns();
        Task<string> EditVehicle(long id, EditVehicleRequest editVehicleRequest);
        Task<VehicleCountResponse> GetVehicleCount();

    }
}
