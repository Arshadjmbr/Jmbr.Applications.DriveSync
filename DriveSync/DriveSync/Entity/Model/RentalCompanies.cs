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
        public short IsActive { get; set; }
        public string? Remarks { get; set; }
        public short Deleted { get; set; } = 0;

    }
}
