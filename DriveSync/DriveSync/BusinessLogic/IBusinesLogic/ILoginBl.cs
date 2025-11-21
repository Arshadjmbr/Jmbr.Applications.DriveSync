using DriveSync.Entity.Request;
using DriveSync.Entity.Response;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface ILoginBl
    {
        Task<TokenResponse> Login(LoginRequest loginRequest);
    }
}
