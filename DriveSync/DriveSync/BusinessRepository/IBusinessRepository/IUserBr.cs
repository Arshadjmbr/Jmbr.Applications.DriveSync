using DriveSync.DTOS.Request;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IUserBr
    {
        Task<string> CreateUser(CreateUserRequest createUserRequest);
        Task<string> CreateDrivers(CreateDriverRequest createDriversRequest);
    }
}
