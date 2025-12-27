using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Handlers.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/v1/Drive-Sync/driver")]
    [ApiController]
    public class DriverController(IDriverBl mDriverBl) : ControllerBase
    {
        [HttpPost]
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

        [HttpGet]
        public async Task<IActionResult> GetDrivers()
        {
            try
            {
                List<GetDriversResponse> response = await mDriverBl.GetDrivers();
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }

        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdowns()
        {
            try
            {
                List<DriversDropdownResponse> response = await mDriverBl.GetDropdowns();
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditDriver(long id, [FromBody] EditDriverRequest editDriverRequest)
        {
            try
            {
                var response = await mDriverBl.EditDriver(id, editDriverRequest);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
