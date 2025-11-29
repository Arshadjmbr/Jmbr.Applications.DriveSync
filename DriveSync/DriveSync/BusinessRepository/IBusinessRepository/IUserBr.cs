using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IUserBr
    {
        Task<string> CreateUser(CreateUserRequest createUserRequest);


    }
}
