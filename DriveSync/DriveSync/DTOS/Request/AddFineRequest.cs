namespace DriveSync.DTOS.Request
{
    public class AddFineRequest
    {
        public long VehicleId { get; set; }
        public string ViolationType { get; set; }
        public string ReceiptNumber { get; set; }
        public string FineNumber { get; set; }
        public decimal Amount { get; set; }
        public string? Reason { get; set; }
        public DateTimeOffset IssuedDate { get; set; }
    }
}
