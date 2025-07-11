using System.Text.Json.Serialization;

namespace Cupa.Application.Common.Response;
public record AuthResponse
{
    public bool IsAuthenticated { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
    //public DateTime ExpiresOn { get; set; }

    [JsonIgnore]
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}
