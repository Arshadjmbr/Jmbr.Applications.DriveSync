namespace DriveSync.DTOS.Response
{
    public class DashboardListResponse
    {
        public string Name { get; set; }
        public string? StaffIdNum { get; set; }
        public long MobileNumber { get; set; }
        public string? EmiratesId { get; set; }
        public string? PassportNum { get; set; }
        public string LicenseType { get; set; }
        public string EmiratesZone { get; set; }
        public DateTimeOffset JoiningDate { get; set; }
        public short? IsActive { get; set; }
        public string? Remarks { get; set; }
    }
}
