using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Handlers.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/v1/Drive-Sync/fines")]
    [ApiController]
    public class FinesController(IFinesBl mFinesBl) : ControllerBase
    {
        [HttpPost("add-fine")]
        public async Task<IActionResult> AddFine(AddFineRequest addFineRequest)
        {
            try
            {
                var result = await mFinesBl.AddFine(addFineRequest);
                return Ok(result);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateFine(long id, [FromBody]UpdateFineRequest updateFineRequest)
        {
            try
            {
                var result = await mFinesBl.UpdateFine(id, updateFineRequest);
                return Ok(result);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFines([FromQuery] FinePaginationRequest request, string? searchText)
        {
            try
            {
                PaginatedResponse<GetFineResponse> result = await mFinesBl.GetFines(request, searchText);
                return Ok(result);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpGet("vehicles-dropdown")]
        public async Task<IActionResult> GetVehicles()
        {
            try
            {
                List<VehicleDropdownResponse> result = await mFinesBl.GetVehicleDropdown();
                return Ok(result);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
