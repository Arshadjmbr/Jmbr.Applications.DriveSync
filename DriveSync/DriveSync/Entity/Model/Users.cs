namespace DriveSync.Entity.Model
{
    public class Users
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public long MobileNumber { get; set; }
        public string Otp { get; set; }
        public DateTime OtpExpiry { get; set; }
        public int RoleId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDateTime  { get; set; }
        public DateTime UpdatedDateTime  { get; set; }
    }
}
