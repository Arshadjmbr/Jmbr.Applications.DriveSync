using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Handlers.ExceptionHandler;
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

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(CreateVehicleRequest createVehicleRequest)
        {
            try
            {
                var response = await mVehicleBl.CreateVehicle(createVehicleRequest);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message});
            }

        }

        [HttpGet]

        public async Task<IActionResult> GetVehicles()
        {
            try
            {
                List<GetVehicleResponse> response = await mVehicleBl.GetVehicles();
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }

        }

        [HttpGet("dropdown")]

        public async Task<IActionResult> GetVehicleDropdowns()
        {
            try
            {
                List<VehicleDropdownResponse> response = await mVehicleBl.GetVehicleDropdowns();
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }

        }
    }
}
