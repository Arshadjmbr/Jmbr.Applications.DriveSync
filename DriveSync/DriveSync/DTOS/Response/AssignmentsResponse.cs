namespace DriveSync.DTOS.Response
{
    public class AssignmentsResponse
    {
        public int TotalRecords { get; set; }
        public string Name { get; set; }
        public string? StaffIdNum { get; set; }
        public long MobileNumber { get; set; }
        public string VehicleNumber { get; set; }
        public string? EmiratesId { get; set; }
        public string? PassportNum { get; set; }
        public DateTimeOffset? JoiningDate { get; set; }
        public DateTimeOffset? StartDateTime { get; set; }
        public DateTimeOffset? EndDateTime { get; set; }
        public string Status { get; set; }
        public string? Remarks { get; set; }

    }
}
