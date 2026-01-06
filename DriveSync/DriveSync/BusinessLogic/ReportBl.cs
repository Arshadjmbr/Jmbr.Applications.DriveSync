using DriveSync.BusinessLogic.IBusinesLogic;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessLogic
{
    public class ReportBl(IReportBr mReportBr):IReportBl
    {
        public async Task<List<FineReportResponse>> GetFineReportData(FineReportRequest fineReportRequest)
        {
            return await mReportBr.GetFineReportData(fineReportRequest);
        }
        public async Task<MemoryStream> GetFineReport(List<FineReportResponse> data)
        {
            return await mReportBr.GetFineReport(data);
        }
        public async Task<Byte[]> GetFineReportPdf(List<FineReportResponse> data)
        {
            return await mReportBr.GetFineReportPdf(data);
        }
    }
}
