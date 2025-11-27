using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DTOS.Response;
using DriveSync.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/v1/Drive-Sync/vehicle")]
    [ApiController]
    public class VehicleController(IVehicleBl mVehicleBl) : ControllerBase
    {
        [HttpGet("vehicle-types")]
        public async Task<IActionResult> GetVehicleTypes()
        {
            try
            {
                List<VehicleTypeResponse> vehicleTypes = await mVehicleBl.GetVehicleTypes();
                return Ok(vehicleTypes);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
