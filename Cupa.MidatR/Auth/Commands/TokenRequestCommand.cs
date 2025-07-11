using Microsoft.AspNetCore.Http;

namespace Cupa.MidatR.Auth.Commands
{
    public class TokenRequestCommand(TokenRequestModelDTO model, HttpResponse responseToken) : IRequest<AuthResponse>
    {
        public TokenRequestModelDTO Model { get; } = model;
        public HttpResponse ResponseToken { get; set; } = responseToken;
    }
}
