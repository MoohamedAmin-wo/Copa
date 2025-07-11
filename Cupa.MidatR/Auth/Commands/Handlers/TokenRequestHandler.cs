namespace Cupa.MidatR.Auth.Commands.Handlers;
public class TokenRequestHandler(UserManager<ApplicationUser> userManager, ITokenGenerator tokenGenerator, IAuthService authService) : IRequestHandler<TokenRequestCommand, AuthResponse>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;
    private readonly IAuthService _authService = authService;
    public async Task<AuthResponse> Handle(TokenRequestCommand request, CancellationToken cancellationToken)
    {
        var response = new AuthResponse();
        var user = await _userManager.FindByEmailAsync(request.Model.email);

        if (user is null)
        {
            response.Message = ErrorMessages.UnRegisteredUser;
            return response;
        }

        if (!await _authService.IsUserEmailConfirmedAsync(user))
        {
            response.Message = ErrorMessages.EmailNotconfirmed;
            return response;
        }

        if (!await _authService.LogCurrentUserIntoSystem(user, request.Model.password))
        {
            response.Message = ErrorMessages.InvalidCredinatials;
            return response;
        }

        if (user.IsBlocked)
        {
            response.Message = "Your account has been banned for violating our rules.";
            return response;
        }

        if(user.IsDeleted)
        {
            response.Message = "Your account has been deleted.";
            return response;
        }

        var jwtSecurityToken = await _tokenGenerator.GenerateJwtToken(user);

        response.IsAuthenticated = true;
        response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        response.RefreshTokenExpiration = jwtSecurityToken.ValidTo;

        if (user.RefreshTokens.Any(t => t.IsActive))
        {
            var activeRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.IsActive);
            response.RefreshToken = activeRefreshToken.Token;
            response.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
        }
        else
        {
            var refreshToken = _tokenGenerator.GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;
            response.RefreshTokenExpiration = refreshToken.ExpiresOn;
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);
        }

        if (!string.IsNullOrEmpty(response.RefreshToken))
            _tokenGenerator.SetRefreshTokenInCookies(response.RefreshToken, response.RefreshTokenExpiration, request.ResponseToken);

        return response;
    }
}