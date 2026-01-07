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


        public async Task<List<VehicleReportResponse>> GetVehicleReportData(VehicleReportRequest vehicleReportRequest)
        {
            return await mReportBr.GetVehicleReportData(vehicleReportRequest);
        }
        public async Task<MemoryStream> GetVehicleReport(List<VehicleReportResponse> data)
        {
            return await mReportBr.GetVehicleReport(data);
        }
        public async Task<Byte[]> GetVehicleReportPdf(List<VehicleReportResponse> data)
        {
            return await mReportBr.GetVehicleReportPdf(data);
        }

        public async Task<List<GetDriversResponse>> GetDriverReportData(DriverReportRequest driverReportRequest)
        {
            return await mReportBr.GetDriverReportData(driverReportRequest);
        }
        public async Task<MemoryStream> GetDriverReport(List<GetDriversResponse> data)
        {
            return await mReportBr.GetDriverReport(data);
        }
        public async Task<Byte[]> GetDriverReportPdf(List<GetDriversResponse> data)
        {
            return await mReportBr.GetDriverReportPdf(data);
        }
    }
}
