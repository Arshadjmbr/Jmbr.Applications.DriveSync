using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface ILoginBr
    {
        Task<TokenResponse> Login(LoginRequest loginRequest);

    }
}
