using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface ILoginBl
    {
        Task<TokenResponse> Login(LoginRequest loginRequest);
    }
}
