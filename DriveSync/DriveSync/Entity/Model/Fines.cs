namespace DriveSync.Entity.Model
{
    public class Fines
    {
        public long Id { get; set; }
        public long DriverId { get; set; }
        public long VehicleId { get; set; }
        public string ViolationType { get; set; }
        public string ReceiptNumber { get; set; }
        public string FineNumber { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTimeOffset IssuedDate { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset? UpdatedDateTime { get; set; }
        public int Paid { get; set; } 
        public DateOnly? PaidDate { get; set; }
    }
}
