using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IFinesBl
    {
        Task<string> AddFine(AddFineRequest addFineRequest);
        Task<string> UpdateFine(long fineId, UpdateFineRequest updateFineRequest);
        Task<List<GetFineResponse>> GetFines();

    }
}
