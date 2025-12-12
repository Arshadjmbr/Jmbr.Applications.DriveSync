using DriveSync.Handlers.ExceptionHandler;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DriveSync.Utility
{
    public class TokenUtility
    {
        public static string GenerateJwtToken(string secretKey, string clientId, string audience, string mobileNumber, string password, int expirationInDays, string userId, string roleId)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] hashedKey = SHA256.Create().ComputeHash(keyBytes);

            string sKey = Convert.ToBase64String(hashedKey);

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(sKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, clientId),
            new Claim(JwtRegisteredClaimNames.UniqueName, secretKey),
            new Claim(JwtRegisteredClaimNames.NameId, mobileNumber),
            password != null ? new Claim(JwtRegisteredClaimNames.Sid, password) : new Claim(JwtRegisteredClaimNames.Sid, "password"),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim("userId", userId),
            roleId != null ? new Claim("roleId", roleId) : new Claim("roleId", "roleId")
            };
            var token = new JwtSecurityToken(
                issuer: null,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expirationInDays),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenS = string.Empty;
            try
            {
                tokenS = tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new PlatformException((int)HttpStatusCode.Unauthorized, "Unable to generate token.");
            }

            return tokenS;
        }
    }
}
