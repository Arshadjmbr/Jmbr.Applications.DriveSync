using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;

namespace DriveSync.BusinessRepository.IBusinessRepository
{
    public interface IReportBr
    {
        Task<List<FineReportResponse>> GetFineReportData(FineReportRequest fineReportRequest);
        Task<Byte[]> GetFineReportPdf(List<FineReportResponse> data);
        Task<MemoryStream> GetFineReport(List<FineReportResponse> data);

        Task<List<VehicleReportResponse>> GetVehicleReportData(VehicleReportRequest vehicleReportRequest);
        Task<Byte[]> GetVehicleReportPdf(List<VehicleReportResponse> data);
        Task<MemoryStream> GetVehicleReport(List<VehicleReportResponse> data);
        Task<List<GetDriversResponse>> GetDriverReportData(DriverReportRequest driverReportRequest);
        Task<Byte[]> GetDriverReportPdf(List<GetDriversResponse> data);
        Task<MemoryStream> GetDriverReport(List<GetDriversResponse> data);

    }
}
