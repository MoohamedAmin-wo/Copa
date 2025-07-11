namespace Cupa.MidatR.Auth.Commands.Handlers;
internal sealed class ConfirmForgetPasswordCodeHandler(IAuthService authService) : IRequestHandler<ConfirmForgetPasswordCodeCommand, GlobalResponseDTO>
{
    private readonly IAuthService _authService = authService;
    public async Task<GlobalResponseDTO> Handle(ConfirmForgetPasswordCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await _authService.GetUserByEmailAsync(request.Model.Email);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _authService.ConfirmUserEmailAsync(user, request.Model.Code))
            return new GlobalResponseDTO { Message = ErrorMessages.InvalidConfirmationCode };

        return new GlobalResponseDTO { IsSuccess = true, Message = "Verfication Compelete" };
    }
}