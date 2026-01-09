using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IFinesBl
    {
        Task<string> AddFine(AddFineRequest addFineRequest);
        Task<string> UpdateFine(long fineId, UpdateFineRequest updateFineRequest);
        Task<List<VehicleDropdownResponse>> GetVehicleDropdown();
        Task<PaginatedResponse<GetFineResponse>> GetFines([FromQuery] FinePaginationRequest request, string? searchText);


    }
}
