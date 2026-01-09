using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<PaginatedResponse<GetVehicleResponse>> GetVehicles([FromQuery] FinePaginationRequest request, string? searchText)
        {
            return await mVehicleBr.GetVehicles(request, searchText);
        }

        public async Task<List<VehicleDropdownResponse>> GetVehicleDropdowns()
        {
            return await mVehicleBr.GetVehicleDropdowns();
        }
        public async Task<string> EditVehicle(long id, EditVehicleRequest editVehicleRequest)
        {
            return await mVehicleBr.EditVehicle(id, editVehicleRequest);
        }
    }
}
