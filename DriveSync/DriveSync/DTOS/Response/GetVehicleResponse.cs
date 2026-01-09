namespace DriveSync.DTOS.Response
{
    public class GetVehicleResponse
    {
        public int TotalRecords { get; set; }
        public long Id { get; set; }
        public string PlateNumber { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string CompanyName { get; set; }
        public string? Remarks { get; set; }
        public int Status { get; set; }
        public DateTime MulkiyaExpiryDate { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset UpdatedDateTime { get; set; }
        public int Deleted { get; set; }
    }
}
