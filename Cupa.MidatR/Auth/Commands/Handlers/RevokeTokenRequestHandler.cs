namespace Cupa.MidatR.Auth.Commands.Handlers;
public sealed class RevokeTokenRequestHandler(ITokenGenerator tokenGenerator) : IRequestHandler<RevokeTokenRequestCommand, bool>
{
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;
    public async Task<bool> Handle(RevokeTokenRequestCommand request, CancellationToken cancellationToken)
    {
        var token = request.Model!.Token ?? request.HttpRequest.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(token))
            return false;

        var result = await _tokenGenerator.RevokeTokenAsync(token);
        if (!result)
            return false;

        return true;
    }
}