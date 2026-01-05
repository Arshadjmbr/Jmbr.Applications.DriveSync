using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

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
        public async Task<List<GetFineResponse>> GetFines()
        {
            return await mFinesBr.GetFines();
        }
    }
}
