namespace Cupa.MidatR.Auth.Commands.Handlers;
internal sealed class RefreshTokenRequestHandler(ITokenGenerator tokenGenerator) : IRequestHandler<RefreshTokenRequestCommand, AuthResponse>
{
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<AuthResponse> Handle(RefreshTokenRequestCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return new AuthResponse
            {
                IsAuthenticated = false,
                Message = ErrorMessages.UnHandledServerError
            };
        }

        var refreshToken = request.ModelRequest.Cookies["refreshToken"];
        var result = await _tokenGenerator.RefreshTokenAsync(refreshToken);

        if (!result.IsAuthenticated)
        {
            return new AuthResponse
            {
                IsAuthenticated = false,
                Message = result.Message
            };
        }

        _tokenGenerator.SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiration, request.ModelResponse);
        return new AuthResponse
        {
            Message = string.Empty,
            IsAuthenticated = true,
            Token = result.Token,
            RefreshTokenExpiration = result.RefreshTokenExpiration
        };
    }
}