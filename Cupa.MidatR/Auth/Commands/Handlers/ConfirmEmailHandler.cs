namespace Cupa.MidatR.Auth.Commands.Handlers;
public class ConfirmEmailHandler(IAuthService authService) : IRequestHandler<ConfirmEmailCommand, GlobalResponseDTO>
{
    private readonly IAuthService _authService = authService;
    public async Task<GlobalResponseDTO> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _authService.GetUserByEmailAsync(request.Model.Email);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnRegisteredUser };

        if (await _authService.IsUserEmailConfirmedAsync(user))
            return new GlobalResponseDTO { Message = ErrorMessages.EmailConfirmed };

        if (!await _authService.ConfirmUserEmailAsync(user, request.Model.Code))
            return new GlobalResponseDTO { Message = ErrorMessages.InvalidConfirmationCode };

        try
        {
            await _authService.AddUserToRoleAsync(user, CupaRoles.User);
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };
        }

        return new GlobalResponseDTO { IsSuccess = true, Message = "Email confirmed Successfully " };
    }
}