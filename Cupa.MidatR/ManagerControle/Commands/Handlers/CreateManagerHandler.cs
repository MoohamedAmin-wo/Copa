namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
public class CreateManagerHandler : IRequestHandler<CreateManagerCommand, AuthResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenGenerator _tokenGenerator;

    public CreateManagerHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ITokenGenerator tokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponse> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new AuthResponse { Message = ErrorMessages.UserNotFound };

        if (user.Age < 40)
            return new AuthResponse { Message = "Managers can't be less than 40 years old !" };

        var isAlreadManager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
        if (isAlreadManager != null)
            return new AuthResponse { Message = "you are already an Manager !" };

        if (await _userManager.IsInRoleAsync(user, CupaRoles.Manager))
            return new AuthResponse { Message = "can't join as manager for 2 clubs !" };

        if (
             await _userManager.IsInRoleAsync(user, CupaRoles.Moderator) ||
             await _userManager.IsInRoleAsync(user, CupaRoles.Admin) ||
             await _userManager.IsInRoleAsync(user, CupaRoles.Player)
           )
            return new AuthResponse { Message = "you are not allowed to join this services !" };

        var manager = new Manager
        {
            UserId = user.Id,
            StoryAbout = request.Model.StoryAbout,
            StoryWithClub = request.Model.StoryWithClub,
            AppointmentDate = request.Model.AppoitmentDate,
            ContractEndsOn = request.Model.contractEndsOn
        };

        var result = await _userManager.AddToRoleAsync(user, CupaRoles.Manager);
        if (!result.Succeeded)
            return new AuthResponse { Message = string.Join("\n", result.Errors.Select(x => x.Description)) };

        try
        {
            await _unitOfWork.managers.CreateAsync(manager);
        }
        catch (Exception ex)
        {
            return new AuthResponse { Message = string.Join("\n", ex.Message) };
        }

        await _unitOfWork.CommitAsync();


        var response = new AuthResponse();
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

        return response;
    }
}
