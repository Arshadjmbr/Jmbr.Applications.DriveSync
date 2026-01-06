namespace DriveSync.DTOS.Response
{
    public class FineReportResponse
    {
        public string StaffIdNum { get; set; }
        public string PlateNumber { get; set; }
        public string Name { get; set; }
        public DateTimeOffset IssuedDate { get; set; }
        public string ViolationType {  get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public string FineNumber { get; set; }
        public int Paid { get; set; }
    }
}
