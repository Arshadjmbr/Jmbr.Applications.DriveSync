using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.Entity.Request;
using DriveSync.Entity.Response;

namespace DriveSync.BusinessLogic
{
    public class LoginBl(ILoginBr mLoginBr):ILoginBl
    {
        public async Task<TokenResponse> Login(LoginRequest loginRequest)
        {
            return await mLoginBr.Login(loginRequest);
        }
    }
}
