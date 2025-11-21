using DriveSync.Entity.Request;
using DriveSync.Entity.Response;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface ILoginBr
    {
        Task<TokenResponse> Login(LoginRequest loginRequest);

    }
}
