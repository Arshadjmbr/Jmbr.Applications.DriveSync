using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.BusinessLogic
{
    public class FinesBl(IFinesBr mFinesBr):IFinesBl
    {
        public async Task<string> AddFine(AddFineRequest addFineRequest)
        {
            return await mFinesBr.AddFine(addFineRequest);
        }
        public async Task<string> UpdateFine(long fineId, UpdateFineRequest updateFineRequest)
        {
            return await mFinesBr.UpdateFine(fineId, updateFineRequest);
        }
        public async Task<PaginatedResponse<GetFineResponse>> GetFines([FromQuery] FinePaginationRequest request, string? searchText)
        {
            return await mFinesBr.GetFines(request, searchText);
        }
        
        public async Task<List<VehicleDropdownResponse>> GetVehicleDropdown()
        {
            return await mFinesBr.GetVehicleDropdown();
        }
    }
}
