namespace DriveSync.Entity.Model
{
    public class RentalCompanies
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public long ContactNumber { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset UpdatedDateTime { get; set; }
        public string? Remarks { get; set; }
        public int Status { get; set; }
        public int Deleted { get; set; } = 0;

    }
}
