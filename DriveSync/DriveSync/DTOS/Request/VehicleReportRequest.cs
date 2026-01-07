namespace DriveSync.DTOS.Request
{
    public class VehicleReportRequest
    {
        public string ExportFormat { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public double? Offset { get; set; }
        //public int? vehicleType { get; set; }
        public string? searchText { get; set; }
    }
}
