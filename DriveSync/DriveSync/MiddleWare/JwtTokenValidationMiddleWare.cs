using DriveSync.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace DriveSync.MiddleWare
{
    public class JwtTokenValidationMiddleWare
    {
        private readonly RequestDelegate mRequestDelegate;
        private readonly IConfiguration mConfiguration;

        public JwtTokenValidationMiddleWare(RequestDelegate next, IConfiguration configuration)
        {
            mRequestDelegate = next;
            mConfiguration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            List<string> thirdPartyAPIs = mConfiguration["Auth:ApiEndPoints"].Split(",").ToList();
            if (!thirdPartyAPIs.Contains(context.Request.Path))
            {
                string? jwtToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Substring("Bearer ".Length);
                var tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = null;
                try
                {
                    token = tokenHandler.ReadJwtToken(jwtToken);
                }
                catch (Exception e)
                {
                    await UnAuthorizedErrorResponse(context, "UnAuthorized: Invalid/Empty Token.");
                    return;
                }

                // Extract and return the claims from the token
                List<string> claimTypes = new List<string>() { TokenConstants.TOKEN_CLAIM_TYPE_EXPIRY_KEY, TokenConstants.HTTP_HEADER_USER_ID_KEY, TokenConstants.TOKEN_CLAIM_TYPE_CLIENT_ID_KEY, TokenConstants.TOKEN_CLAIM_TYPE_SECRET_ID_KEY, TokenConstants.HTTP_HEADER_ROLE_ID_KEY, TokenConstants.TOKEN_CLAIM_TYPE_ROLE_KEY };

                ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, TokenConstants.TOKEN_TYPE));
                var claimsDictionary = principal.Claims.Where(c => claimTypes.Contains(c.Type)).ToDictionary(
                                        claim => claim.Type,
                                        claim => claim.Value
                                    );

                _ = claimsDictionary.TryGetValue(TokenConstants.TOKEN_CLAIM_TYPE_CLIENT_ID_KEY, out string? clientId);
                _ = claimsDictionary.TryGetValue(TokenConstants.TOKEN_CLAIM_TYPE_SECRET_ID_KEY, out string? clientSecret);
                _ = claimsDictionary.TryGetValue(TokenConstants.TOKEN_CLAIM_TYPE_EXPIRY_KEY, out string? tokenExpiry);
                _ = claimsDictionary.TryGetValue(TokenConstants.HTTP_HEADER_USER_ID_KEY, out string? userId);
                _ = claimsDictionary.TryGetValue(TokenConstants.HTTP_HEADER_ROLE_ID_KEY, out string? roleId);
                _ = claimsDictionary.TryGetValue(TokenConstants.TOKEN_CLAIM_TYPE_ROLE_KEY, out string? userRole);


                if (clientId != string.Concat(TokenConstants.TOKEN_CLAIM_TYPE_CLIENT_ID_VALUE, "-", userId) && clientSecret != TokenConstants.TOKEN_CLAIM_TYPE_SECRET_ID_VALUE)
                {
                    await UnAuthorizedErrorResponse(context, "UnAuthorized: Invalid Token.");
                    return;
                }

                long expiryTime = long.Parse(tokenExpiry);
                DateTime expiryDateTime = DateTimeOffset.FromUnixTimeSeconds(expiryTime).UtcDateTime;
                DateTime currentUtcTime = DateTime.UtcNow;
                if (currentUtcTime > expiryDateTime)
                {
                    await UnAuthorizedErrorResponse(context, "UnAuthorized: Token Expired.");
                    return;
                }

                context.Items[TokenConstants.HTTP_HEADER_USER_ID_KEY] = userId;
                context.Items[TokenConstants.HTTP_HEADER_ROLE_ID_KEY] = roleId;
                context.Items[TokenConstants.HTTP_HEADER_USER_ROLE_KEY] = userRole;

            }
            await mRequestDelegate(context); // Continue processing the request
        }

        private static async Task UnAuthorizedErrorResponse(HttpContext context, string message)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                statusCode = 401,
                message = message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
