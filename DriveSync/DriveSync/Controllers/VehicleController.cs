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
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }

        }

        [HttpGet]

        public async Task<IActionResult> GetVehicles([FromQuery] FinePaginationRequest request, string? searchText)
        {
            try
            {
                PaginatedResponse<GetVehicleResponse> response = await mVehicleBl.GetVehicles(request, searchText);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> EditVehicle(long id, [FromBody] EditVehicleRequest editVehicleRequest)
        {
            try
            {
                var response = await mVehicleBl.EditVehicle(id, editVehicleRequest);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetVehicleCount()
        {
            try
            {
                var response = await mVehicleBl.GetVehicleCount();
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(long id)
        {
            try
            {
                var response = await mVehicleBl.DeleteVehicle(id);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
