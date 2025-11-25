namespace DriveSync.DTOS.Request
{
    public class CreateDriverRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string? StaffIdNum { get; set; }
        public string? EmailAddress { get; set; }
        public long MobileNumber { get; set; }
        public string? EmiratesId { get; set; }
        public string? PassportNum { get; set; }
        public string LicenseType { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public string EmiratesZone { get; set; }
        public DateTimeOffset JoiningDate { get; set; }
        public string? Remarks { get; set; }
    }
}
