using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;

namespace DriveSync.BusinessLogic
{
    public class UserBl(IUserBr mUserBr):IUserBl
    {
        public async Task<string> CreateUser(Entity.Request.CreateUserRequest createUserRequest)
        {
            return await mUserBr.CreateUser(createUserRequest);
        }
    }
}
