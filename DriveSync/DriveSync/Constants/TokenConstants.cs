namespace DriveSync.Constants
{
    public class TokenConstants
    {
        public const string CLIENT_ID_KEY = "clientId";
        public const string CLIENT_SECRET_KEY = "clientSecret";

        public const string TOKEN_TYPE = "jwt";

        public const string TOKEN_CLAIM_TYPE_ROLE_KEY = "nameid";
        public const string TOKEN_CLAIM_TYPE_EXPIRY_KEY = "exp";
        public const string TOKEN_CLAIM_TYPE_USER_ID_KEY = "sid";

        public const string TOKEN_CLAIM_TYPE_CLIENT_ID_KEY = "sub";
        public const string TOKEN_CLAIM_TYPE_SECRET_ID_KEY = "unique_name";

        public const string TOKEN_CLAIM_TYPE_CLIENT_ID_VALUE = "DriveSync";
        public const string TOKEN_CLAIM_TYPE_SECRET_ID_VALUE = "DriveSync-2025";


        public const string HTTP_HEADER_USER_ID_KEY = "userId";
        public const string HTTP_HEADER_ROLE_ID_KEY = "roleId";
        public const string HTTP_HEADER_USER_ROLE_KEY = "UserRole";
    }
}
