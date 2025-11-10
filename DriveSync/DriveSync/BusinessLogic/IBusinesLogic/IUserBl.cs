using DriveSync.Entity.Request;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IUserBl
    {
        Task<string> CreateUser(CreateUserRequest createUserRequest);
    }
}
