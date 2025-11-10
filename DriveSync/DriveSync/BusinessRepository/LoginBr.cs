using Azure.Core;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.Constants;
using DriveSync.DBContext;
using DriveSync.Entity.Model;
using DriveSync.Entity.Request;
using DriveSync.Entity.Response;
using DriveSync.Hashing;
using DriveSync.Utility;
using System.Runtime.CompilerServices;

namespace DriveSync.BusinessRepository
{
    public class LoginBr(DriveSyncDbContext mDriveSyncDbContext, IConfiguration mConfiguration, IHttpContextAccessor mHttpContextAccessor) :ILoginBr
    {
        public async Task<TokenResponse> Login (LoginRequest loginRequest)
        {
            Users? user = mDriveSyncDbContext.Users.Where(u => u.EmailAddress == loginRequest.EmailAddress).FirstOrDefault();
            string hashedPassword = PasswordHashing.HashingPassword(loginRequest.Password);
            if (user == null)
            {
                throw new BadHttpRequestException("Invalid Credentials!");
            }
            if (user.Password != hashedPassword)
            {
                throw new BadHttpRequestException("Invalid Credentials!");
            }

            string clientIdKey = mHttpContextAccessor.HttpContext.Request.Headers[TokenConstants.CLIENT_ID_KEY].ToString();
            string clientSecretKey = mHttpContextAccessor.HttpContext.Request.Headers[TokenConstants.CLIENT_SECRET_KEY].ToString();

            string clientId = mConfiguration["APIAuthentication:clientIdKey"];
            string clientSecret = mConfiguration["APIAuthentication:clientSecretKey"];
            string decodeclientIdKey = DecryptionUtility.Base64Decode(clientIdKey);
            string decodedclientSecretKey = DecryptionUtility.Base64Decode(clientSecretKey);

            if (decodeclientIdKey != TokenConstants.TOKEN_CLAIM_TYPE_CLIENT_ID_VALUE && decodedclientSecretKey != TokenConstants.TOKEN_CLAIM_TYPE_SECRET_ID_VALUE)
            {
                throw new UnauthorizedAccessException("Invalid client credentials");
            }

            string scope = mConfiguration["AuthUrl:Audience"];
            int expirationInDays = int.Parse(mConfiguration["AuthUrl:AccessTokenLifetime"]);
            int refreshTokenExpiryInDays = int.Parse(mConfiguration["AuthUrl:AbsoluteRefreshTokenLifetime"]);
            string jwtToken = TokenUtility.GenerateJwtToken(mConfiguration["APIAuthentication:clientSecretKey"], mConfiguration["APIAuthentication:clientIdkey"], scope, loginRequest.EmailAddress, user.Password, expirationInDays, user.Id.ToString(), user.RoleId.ToString());
            string refreshToken = TokenUtility.GenerateJwtToken(mConfiguration["APIAuthentication:clientSecretKey"], mConfiguration["APIAuthentication:clientIdkey"], scope, loginRequest.EmailAddress, user.Password, refreshTokenExpiryInDays, user.Id.ToString(), user.RoleId.ToString());
            TokenResponse token = new TokenResponse()
            {
                AccessToken = jwtToken,
                RefreshToken = refreshToken,
                ExpiresIn = expirationInDays,
                UserId = user.Id,
                RoleId = user.RoleId,
            };
            return token;
        }
    }
}
