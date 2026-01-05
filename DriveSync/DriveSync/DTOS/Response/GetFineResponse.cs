namespace DriveSync.DTOS.Response
{
    public class GetFineResponse
    {
        public long Id { get; set; }
        public string FineNumber { get; set; }
        public decimal Amount { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTimeOffset IssuedDate { get; set; }
        public string ViolationType { get; set; }
        public int Paid { get; set; }
        public DateTime? PaidDate { get; set; }
        public string Reason { get; set; }
        public string DriverName { get; set; }
        public string VehiclePlateNumber { get; set; }
    }
}
