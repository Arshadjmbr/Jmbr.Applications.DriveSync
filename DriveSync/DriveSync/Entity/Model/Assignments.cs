namespace DriveSync.Entity.Model
{
    public class Assignments
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public long DriverId { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset? EndDateTime { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset UpdatedDateTime { get; set;}
        public string? Comments { get; set; }
        public string? Remarks { get; set; }
        public int Status { get; set; }
    }
}
