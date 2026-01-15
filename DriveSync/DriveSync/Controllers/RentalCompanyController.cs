using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Handlers.ExceptionHandler;
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
        public async Task<IActionResult> GetRentalCompanies([FromQuery] FinePaginationRequest request, string? searchText)
        {
            try
            {
                PaginatedResponse<RentalCompanyResponse> response = await mRentalCompanyBl.GetRentalCompanies(request, searchText);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRentalCompany(long id, [FromBody] UpdateCompanyRequest request)
        {
            try
            {
                var result = await mRentalCompanyBl.UpdateCompany(id, request);
                return Ok(new { message = result });
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(long id)
        {
            try
            {
                var response = await mRentalCompanyBl.DeleteCompany(id);
                return Ok(response);
            }
            catch (PlatformException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
