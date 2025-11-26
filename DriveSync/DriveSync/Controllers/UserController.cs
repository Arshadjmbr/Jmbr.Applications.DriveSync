using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/v1/Drive-Sync/users")]
    [ApiController]
    public class UserController(IUserBl mUserBl) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest createUserRequest)
        {
            try
            {
                var response = await mUserBl.CreateUser(createUserRequest);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpPost("drivers")]
        public async Task<IActionResult> CreateDrivers(CreateDriverRequest createDriversRequest)
        {
            try
            {
                var response = await mUserBl.CreateDrivers(createDriversRequest);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpGet("zone")]

        public async Task<IActionResult> GetZone()
        {
            try
            {
                List<EmirateZoneResponse> response = await mUserBl.GetZone();
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }

        }

        [HttpGet("license-type")]
        public async Task<IActionResult> GetLicenseType()
        {
            try
            {
                List<LicenseTypeResponse> response = await mUserBl.GetLicenseType();
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
