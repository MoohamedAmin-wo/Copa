namespace Cupa.Application.Services.TokenServices
{
    public interface ITokenGenerator
    {
        void SetRefreshTokenInCookies(string refreshToken, DateTime expireOn, HttpResponse response);
        Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user);
        Task<AuthResponse> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        RefreshToken GenerateRefreshToken();

        string GetEmailFromToken(string token);
        string GetUserIdFromToken(string token);
    }
}
