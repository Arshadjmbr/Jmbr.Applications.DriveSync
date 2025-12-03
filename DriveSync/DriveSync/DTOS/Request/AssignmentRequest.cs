namespace DriveSync.DTOS.Request
{
    public class AssignmentRequest
    {
        public long VehicleId { get; set; }
        public long DriverId { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset? EndDateTime { get; set; }
        public string? Comments { get; set; }
        public string? Remarks { get; set; }
    }
}
