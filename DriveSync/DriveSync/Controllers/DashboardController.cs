using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/v1/Drive-Sync/dashboard")]
    [ApiController]
    public class DashboardController(IDashboardBl mDashboardBl) : ControllerBase
    {
        //[HttpGet("list")]
        //public async Task<IActionResult> GetDashboardList()
        //{
        //    try
        //    {
        //        var response = await mDashboardBl.GetDashboardList();
        //        return Ok(response);
        //    }
        //    catch (PlatformException ex)
        //    {
        //        return StatusCode(ex.StatusCode, new { error = ex.Message });
        //    }
        //}

        //[HttpPost("assignments")]

    }
}
