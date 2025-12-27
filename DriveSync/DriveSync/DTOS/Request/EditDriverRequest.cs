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
        //public long AlternateMobileNumber { get; set; }
        public string? EmiratesId { get; set; }
        public string? PassportNum { get; set; }
        public int? LicenseTypeId { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public int? EmiratesZoneId { get; set; }
        public DateTimeOffset? JoiningDate { get; set; }
        public DateTimeOffset? VacationstartDate { get; set; }
        public DateTimeOffset? VacationEndDate { get; set; }
        public DateTimeOffset? ResignationDate { get; set; }
        public string? Remarks { get; set; }
    }
}
