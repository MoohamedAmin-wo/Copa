namespace Cupa.MidatR.Auth.Commands.Handlers;
internal sealed class RequestCodeHandler(IAuthService authService) : IRequestHandler<RequestCodeCommand, GlobalResponseDTO>
{
    private readonly IAuthService _authService = authService;
    public async Task<GlobalResponseDTO> Handle(RequestCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await _authService.GetUserByEmailAsync(request.Model.email);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnRegisteredUser };

        var subject = string.Empty;

        if (request.RequestType.Equals(CodeType.forget))
            subject = "Forget password Code";

        if (request.RequestType.Equals(CodeType.confirm))
        {
            if (await _authService.IsUserEmailConfirmedAsync(user))
                return new GlobalResponseDTO { Message = ErrorMessages.EmailConfirmed };

            subject = "Confirmation code";
        }

        var code = await _authService.GenerateNewConfirmationCodeAsync(user);

        var sendCodeResult = await _authService.SendEmailToUserAsync(request.Model.email
            , subject, $"<h2>your Code is : {code}<p>this code is valid for 2 minutes</p></h2>");

        if (!string.IsNullOrEmpty(sendCodeResult))
            return new GlobalResponseDTO { Message = sendCodeResult };

        return new GlobalResponseDTO { Message = "Check inbox for your code !" };
    }
}