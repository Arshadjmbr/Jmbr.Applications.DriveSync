using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;

namespace DriveSync.BusinessLogic
{
    public class RentalCompanyBl(IRentalCompanyBr mRentalCompanyBr):IRentalCompanyBl
    {
        public async Task<string> Register(RegisterCompaniesRequest request)
        {
            return await mRentalCompanyBr.Register(request);
        }
    }
}
