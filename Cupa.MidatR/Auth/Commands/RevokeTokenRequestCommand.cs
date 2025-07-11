using Microsoft.AspNetCore.Http;
namespace Cupa.MidatR.Auth.Commands;
public sealed class RevokeTokenRequestCommand(RevokeTokenModelDTO? model, HttpRequest httpRequest) : IRequest<bool>
{
    public RevokeTokenModelDTO? Model { get; } = model;
    public HttpRequest HttpRequest { get; } = httpRequest;
}