namespace DriveSync.Entity.Model
{
    public class Drivers
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string? StaffIdNum { get; set; }
        public string? Email { get; set; }
        public long MobileNumber { get; set; }
        public long? AlternateMobileNumber { get; set; }
        public string? EmiratesId { get; set; }
        public string PassportNum { get; set; }
        public int LicenseTypeId { get; set; }
        public string LicenseNumber { get; set; }
        public DateOnly LicenseExpiryDate { get; set; }
        public int EmiratesZoneId { get; set; }
        public DateOnly JoiningDate { get; set; }
        public DateOnly? ResignationDate { get; set; }
        public DateOnly? VacationDateFrom { get; set; }
        public DateOnly? VacationDateTo { get; set; }
        public int Status { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset? UpdatedDateTime { get; set; }
        public string? Remarks { get; set; }
        public int Deleted { get; set; } = 0;
    }

}
