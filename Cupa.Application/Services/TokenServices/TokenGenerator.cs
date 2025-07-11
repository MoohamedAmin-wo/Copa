using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Cupa.Application.Services.TokenServices
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        public TokenGenerator(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public void SetRefreshTokenInCookies(string refreshToken, DateTime expireOn, HttpResponse response)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expireOn.ToLocalTime()
            };

            response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);
        }

        public async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var Claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub , user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email  , user.Email!),
            new Claim(ClaimTypes.NameIdentifier  , user.Id),
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: Claims,
            expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthResponse> RefreshTokenAsync(string token)
        {
            var response = new AuthResponse();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == token));

            if (user is null)
            {
                response.Message = "Invalid token !";
                return response;
            }

            var refreshToken = user.RefreshTokens!.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                response.Message = "Inactive token !";
                return response;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens!.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await GenerateJwtToken(user);
            response.IsAuthenticated = true;
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            response.RefreshToken = newRefreshToken.Token;
            response.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            return response;
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == token));

            if (user is null)
                return false;

            var refreshToken = user.RefreshTokens!.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
            return true;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var random = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(random);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(random),
                ExpiresOn = DateTime.Now.AddMinutes(30),
                CreatedOn = DateTime.Now
            };
        }

        public string GetEmailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                return string.Empty;

            // Parse the JWT token
            var jwtToken = handler.ReadJwtToken(token);

            // Extract claims
            var claims = jwtToken.Claims;

            return claims.FirstOrDefault(c => c.Type == "email")?.Value!;
        }

        public string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                return string.Empty;

            // Parse the JWT token
            var jwtToken = handler.ReadJwtToken(token);

            // Extract claims
            var claims = jwtToken.Claims;

            return claims.FirstOrDefault(c => c.Type == "jti")?.Value!;
        }
    }
}
