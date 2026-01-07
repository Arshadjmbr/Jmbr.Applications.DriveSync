using Azure.Core;
using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.DTOS.Request;
using DriveSync.Handlers.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSync.Controllers
{
    [Route("api/v1/Drive-Sync/reports")]
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

        [HttpPost("vehicle-report")]
        public async Task<IActionResult> GetVehicleReport(VehicleReportRequest vehicleReportRequest)
        {
            var data = await mReportBl.GetVehicleReportData(vehicleReportRequest);
            if (vehicleReportRequest.ExportFormat.ToLower() == "pdf")
            {
                byte[] pdfBytes = await mReportBl.GetVehicleReportPdf(data);
                string contentType = CommonConstants.PDF_CONTENT_TYPE;
                string fileName = CommonConstants.PDF_VEHICLE_FILE_NAME;
                return File(pdfBytes, contentType, fileName);
            }
            else
            {
                using MemoryStream response = await mReportBl.GetVehicleReport(data);
                string contentType = CommonConstants.EXCEL_CONTENT_TYPE;
                string fileName = CommonConstants.EXCEL_VEHICLE_FILE_NAME;
                return File(response.ToArray(), contentType, fileName);
            }
        }


        [HttpPost("driver-report")]
        public async Task<IActionResult> GettDriverReport(DriverReportRequest driverReportRequest)
        {
            var data = await mReportBl.GetDriverReportData(driverReportRequest);
            if (driverReportRequest.ExportFormat.ToLower() == "pdf")
            {
                byte[] pdfBytes = await mReportBl.GetDriverReportPdf(data);
                string contentType = CommonConstants.PDF_CONTENT_TYPE;
                string fileName = CommonConstants.PDF_DRIVER_FILE_NAME;
                return File(pdfBytes, contentType, fileName);
            }
            else
            {
                using MemoryStream response = await mReportBl.GetDriverReport(data);
                string contentType = CommonConstants.EXCEL_CONTENT_TYPE;
                string fileName = CommonConstants.EXCEL_DRIVER_FILE_NAME;
                return File(response.ToArray(), contentType, fileName);
            }
        }
    }
}
