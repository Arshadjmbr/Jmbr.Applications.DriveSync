namespace DriveSync.DTOS.Request
{
    public class EditDriverRequest
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public short? Age { get; set; }
        public string? StaffIdNum { get; set; }
        public string? EmailAddress { get; set; }
        public long? MobileNumber { get; set; }
        public long? AlternateMobileNumber { get; set; }
        public string? EmiratesId { get; set; }
        public string? PassportNum { get; set; }
        public int? LicenseTypeId { get; set; }
        public string? LicenseNumber { get; set; }
        public DateOnly? LicenseExpiryDate { get; set; }
        public int? EmiratesZoneId { get; set; }
        public DateOnly? JoiningDate { get; set; }
        public DateOnly? VacationstartDate { get; set; }
        public DateOnly? VacationEndDate { get; set; }
        public DateOnly? ResignationDate { get; set; }
        public string? Remarks { get; set; }
    }
}
