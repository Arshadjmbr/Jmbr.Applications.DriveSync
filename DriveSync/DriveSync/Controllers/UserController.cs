using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DBContext;
using DriveSync.Entity.Request;
using DriveSync.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/v1/Drive-Sync/users")]
    [ApiController]
    public class UserController(IUserBl mUserBl): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest createUserRequest)
        {
            try
            {
                string response = await mUserBl.CreateUser(createUserRequest);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
