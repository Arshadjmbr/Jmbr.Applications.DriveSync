using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic.IBusinesLogic
{
    public interface IReportBl
    {
        Task<List<FineReportResponse>> GetFineReportData(FineReportRequest fineReportRequest);
        Task<Byte[]> GetFineReportPdf(List<FineReportResponse> data);
        Task<MemoryStream> GetFineReport(List<FineReportResponse> data);
    }
}
