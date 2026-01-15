using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IFinesBr
    {
        Task<string> AddFine(AddFineRequest addFineRequest);
        Task<string> UpdateFine(long fineId, UpdateFineRequest updateFineRequest);
        Task<PaginatedResponse<GetFineResponse>> GetFines([FromQuery] FinePaginationRequest request, string? searchText);
        Task<List<VehicleDropdownResponse>> GetVehicleDropdown();
        Task<string> DeleteFine(long fineId);


    }
}
