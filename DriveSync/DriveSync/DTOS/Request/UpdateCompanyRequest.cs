namespace DriveSync.DTOS.Request
{
    public class UpdateCompanyRequest
    {
        public long Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public long? ContactNumber { get; set; }
        public string? Remarks { get; set; }
    }
}
