namespace DriveSync.DTOS.Request
{
    public class FineReportRequest
    {
        public string ExportFormat { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public double? Offset { get; set; }
        public int? Status { get; set; }
        public string? searchText { get; set; }
    }
}
