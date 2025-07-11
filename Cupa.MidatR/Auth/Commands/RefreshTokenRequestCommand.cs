using Microsoft.AspNetCore.Http;

namespace Cupa.MidatR.Auth.Commands
{
    public sealed class RefreshTokenRequestCommand(HttpRequest httpRequest, HttpResponse httpResponse) : IRequest<AuthResponse>
    {
        public HttpRequest ModelRequest { get; } = httpRequest;
        public HttpResponse ModelResponse = httpResponse;
    }
}
