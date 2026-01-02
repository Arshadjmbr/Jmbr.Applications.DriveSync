namespace DriveSync.DTOS.Request
{
    public class UpdateAssignment
    {
        public long Id { get; set; }
        public DateTimeOffset? EndDateTime { get; set; }
        public string? Remarks { get; set; }
        public string? Comments { get; set; }
    }
}
