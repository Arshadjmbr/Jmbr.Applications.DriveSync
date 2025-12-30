using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic
{
    public class RentalCompanyBl(IRentalCompanyBr mRentalCompanyBr):IRentalCompanyBl
    {
        public async Task<string> Register(RegisterCompaniesRequest request)
        {
            return await mRentalCompanyBr.Register(request);
        }

        public async Task<List<RentalCompanyResponse>> GetRentalCompanies()
        {
            return await mRentalCompanyBr.GetRentalCompanies();
        }

        public async Task<string> UpdateCompany(long id, UpdateCompanyRequest updateCompanyRequest)
        {
            return await mRentalCompanyBr.UpdateCompany(id, updateCompanyRequest);
        }
    }
}
