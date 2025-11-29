using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController(IDriverBl mDriverBl) : ControllerBase
    {
        [HttpPost("drivers")]
        public async Task<IActionResult> CreateDrivers(CreateDriverRequest createDriversRequest)
        {
            try
            {
                var response = await mDriverBl.CreateDrivers(createDriversRequest);
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
                List<EmirateZoneResponse> response = await mDriverBl.GetZone();
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
                List<LicenseTypeResponse> response = await mDriverBl.GetLicenseType();
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
