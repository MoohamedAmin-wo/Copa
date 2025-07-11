namespace Cupa.MidatR.Auth.Commands.Handlers;
public class ResetPasswordAfterForgetHandler(IAuthService authService) : IRequestHandler<ResetPasswordAfterForgetCommand, GlobalResponseDTO>
{
    private readonly IAuthService _authService = authService;
    public async Task<GlobalResponseDTO> Handle(ResetPasswordAfterForgetCommand request, CancellationToken cancellationToken)
    {
        var user = await _authService.GetUserByEmailAsync(request.Model.email);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnRegisteredUser };

        if (!await _authService.CheckUserPasswordsMatchAsync(request.Model.newPassword, request.Model.confirmationPassword))
            return new GlobalResponseDTO { Message = ErrorMessages.ConfirmPasswordNotMatch };

        var updatePasswordResult = await _authService.UpdateUserPasswordAsync(user, request.Model.newPassword);
        if (!string.IsNullOrEmpty(updatePasswordResult))
            return new GlobalResponseDTO { Message = updatePasswordResult };

        var sendEmailResult = await _authService.SendEmailToUserAsync(request.Model.email, "Notification about your password updates",
            $"<h2>Your password has been updated <h3>at {DateTime.UtcNow}<p>if you didn't do this updates , please notify us</p></h3></h2>");
        if (!string.IsNullOrEmpty(sendEmailResult))
            return new GlobalResponseDTO { Message = sendEmailResult };

        return new GlobalResponseDTO { IsSuccess = true, Message = "Password Reseted Successfully" };
    }
}