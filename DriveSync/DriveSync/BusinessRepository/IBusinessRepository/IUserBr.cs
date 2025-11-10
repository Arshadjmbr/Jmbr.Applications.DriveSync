using DriveSync.Entity.Request;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IUserBr
    {
        Task<string> CreateUser(CreateUserRequest createUserRequest);
    }
}
