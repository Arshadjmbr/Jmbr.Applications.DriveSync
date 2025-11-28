namespace DriveSync.DTOS.Response
{
    public class RentalCompanyResponse
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public long ContactNumber { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset UpdatedDateTime { get; set; }
        public short IsActive { get; set; }
        public string? Remarks { get; set; }
    }
}
