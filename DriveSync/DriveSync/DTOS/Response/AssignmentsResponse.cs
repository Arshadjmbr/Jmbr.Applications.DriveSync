namespace DriveSync.DTOS.Response
{
    public class AssignmentsResponse
    {
        public int ActiveAssignmentsCount { get; set; } 
        public int CompletedAssignmentsCount { get; set; }
        public int TotalRecords { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string? StaffIdNum { get; set; }
        public long MobileNumber { get; set; }
        public string VehicleNumber { get; set; }
        public string? EmiratesId { get; set; }
        public string? PassportNum { get; set; }
        public DateTimeOffset? JoiningDate { get; set; }
        public DateTimeOffset? StartDateTime { get; set; }
        public DateTimeOffset? EndDateTime { get; set; }
        public int Status { get; set; }
        public string? Remarks { get; set; }

    }
}
