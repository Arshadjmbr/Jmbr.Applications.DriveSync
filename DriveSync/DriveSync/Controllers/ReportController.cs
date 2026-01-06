using Azure.Core;
using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DTOS.Request;
using DriveSync.Handlers.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController(IReportBl mReportBl) : ControllerBase
    {
        [HttpPost("fine-report")]
        public async Task<IActionResult> GetFineReport(FineReportRequest fineReportRequest)
        {
            var data = await mReportBl.GetFineReportData(fineReportRequest);
            if (fineReportRequest.ExportFormat.ToLower() == "pdf")
            {
                byte[] pdfBytes = await mReportBl.GetFineReportPdf(data);
                string contentType = CommonConstants.PDF_CONTENT_TYPE;
                string fileName = CommonConstants.PDF_FINE_FILE_NAME;
                return File(pdfBytes, contentType, fileName);
            }
            else
            {
                using MemoryStream response = await mReportBl.GetFineReport(data);
                string contentType = CommonConstants.EXCEL_CONTENT_TYPE;
                string fileName = CommonConstants.EXCEL_FINE_FILE_NAME;
                return File(response.ToArray(), contentType, fileName);
            }
        }
    }
}
