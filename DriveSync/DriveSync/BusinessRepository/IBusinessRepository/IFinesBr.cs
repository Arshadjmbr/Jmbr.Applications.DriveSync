using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IFinesBr
    {
        Task<string> AddFine(AddFineRequest addFineRequest);
        Task<string> UpdateFine(long fineId, UpdateFineRequest updateFineRequest);
        Task<List<GetFineResponse>> GetFines();

    }
}
