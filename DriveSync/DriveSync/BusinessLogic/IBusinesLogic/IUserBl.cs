using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IUserBl
    {
        Task<string> CreateUser(CreateUserRequest createUserRequest);

    }
}
