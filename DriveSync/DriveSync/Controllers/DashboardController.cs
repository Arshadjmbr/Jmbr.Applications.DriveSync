using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Handlers.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/v1/Drive-Sync/dashboard")]
    [ApiController]
    public class DashboardController(IDashboardBl mDashboardBl) : ControllerBase
    {

        [HttpPost("assignments")]
        public async Task<IActionResult> AddAssignemnts(AssignmentRequest assignmentRequest)
        {
            try
            {
                string response = await mDashboardBl.AddAssignments(assignmentRequest);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpGet("all-assignments")]

        public async Task<IActionResult> GetAllAssignments([FromQuery] AssignmentPaginationRequest request, string? searchText)
        {
            PaginatedResponse<AssignmentsResponse> response = await mDashboardBl.GetAllAssignments(request, searchText);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPut("update-assignment/{id}")]
        public async Task<IActionResult> UpdateAssignmentStatus(long id, [FromBody] UpdateAssignment request)
        {
            try
            {
                var response = await mDashboardBl.UpdateAssignment(id, request);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(long id)
        {
            try
            {
                var response = await mDashboardBl.DeleteAssignment(id);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
