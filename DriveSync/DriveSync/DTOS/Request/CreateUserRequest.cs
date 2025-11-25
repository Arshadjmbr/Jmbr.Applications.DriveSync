namespace DriveSync.DTOS.Request
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int? Age { get; set; }
        public long MobileNumber { get; set; }
        public int RoleId { get; set; }
    }
}
