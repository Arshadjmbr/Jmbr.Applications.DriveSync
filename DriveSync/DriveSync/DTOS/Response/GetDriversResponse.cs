namespace DriveSync.DTOS.Response
{
    public class GetDriversResponse
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string? StaffIdNum { get; set; }
        public string? Email { get; set; }
        public long MobileNumber { get; set; }
        public string? EmiratesId { get; set; }
        public string? PassportNum { get; set; }
        public string LicenseType { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public string EmirateZone { get; set; }
        public DateTimeOffset JoiningDate { get; set; }
        public DateTimeOffset? ResignationDate { get; set; }
        public DateTime? VacationDateFrom { get; set; }
        public DateTime? VacationDateTo { get; set; }
        public short? IsActive { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset? UpdatedDateTime { get; set; }
        public string? Remarks { get; set; }
        public int Deleted { get; set; }
    }
}
