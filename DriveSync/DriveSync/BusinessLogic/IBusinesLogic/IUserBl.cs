using DriveSync.DTOS.Request;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IUserBl
    {
        Task<string> CreateUser(CreateUserRequest createUserRequest);
        Task<string> CreateDrivers(CreateDriverRequest createDriversRequest);
    }
}
