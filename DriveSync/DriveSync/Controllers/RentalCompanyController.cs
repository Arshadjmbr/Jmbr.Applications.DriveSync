using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DTOS.Request;
using DriveSync.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/v1/Drive-Sync/rentalCompany")]
    [ApiController]
    public class RentalCompanyController(IRentalCompanyBl mRentalCompanyBl) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCompaniesRequest rentalCompanyRequest)
        {
            try
            {
                var response = await mRentalCompanyBl.Register(rentalCompanyRequest);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetRentalCompanies()
        {
            try
            {
                var response = await mRentalCompanyBl.GetRentalCompanies();
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
