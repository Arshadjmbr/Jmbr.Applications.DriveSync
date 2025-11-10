namespace DriveSync.Entity.Response
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int? ExpiresIn { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
    }
}
