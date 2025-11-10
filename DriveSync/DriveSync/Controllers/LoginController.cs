using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.Entity.Request;
using DriveSync.Entity.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/v1/Drive-Sync/auth")]
    [ApiController]
    public class LoginController(ILoginBl mLoginBl) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login ([FromBody] LoginRequest loginRequest)
        {
            try
            {
                TokenResponse response = await mLoginBl.Login(loginRequest);
                return Ok(response);
            }
            catch(BadHttpRequestException ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }

        }
    }
}
