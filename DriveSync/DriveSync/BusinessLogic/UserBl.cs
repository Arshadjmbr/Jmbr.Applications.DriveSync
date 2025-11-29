using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic
{
    public class UserBl(IUserBr mUserBr):IUserBl
    {
        public async Task<string> CreateUser(CreateUserRequest createUserRequest)
        {
            return await mUserBr.CreateUser(createUserRequest);
        }
      
    }
}
