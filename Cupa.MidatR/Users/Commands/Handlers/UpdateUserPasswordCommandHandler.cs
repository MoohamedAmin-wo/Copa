namespace Cupa.MidatR.Users.Commands.Handlers;
internal sealed class UpdateUserPasswordCommandHandler(UserManager<ApplicationUser> userManager, IEmailSender emailSender) : IRequestHandler<UpdateUserPasswordCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailSender _emailSender = emailSender;
    public async Task<GlobalResponseDTO> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        //if(user.LastUpdateForPassword < DateTime.UtcNow.AddDays(1))
        //{
        //    var allowedUpdates = DateTime.UtcNow.AddDays(1) - user.LastUpdateForPassword ;
        //    return new GlobalResponseDTO { Message = "unable to update Password now , \n try again in {}" };
        //}

        var checkPassword = await _userManager.CheckPasswordAsync(user, request.Model.OldPassword);
        if (!checkPassword)
            return new GlobalResponseDTO { Message = ErrorMessages.InvalidPassword };

        if (await _userManager.CheckPasswordAsync(user, request.Model.NewPassword))
            return new GlobalResponseDTO { Message = ErrorMessages.OldPasswordMatchesNewPassword };

        if (request.Model.NewPassword != request.Model.ConfirmationPassword)
            return new GlobalResponseDTO { Message = ErrorMessages.ConfirmPasswordNotMatch };

        var UpdatePasswordResult = await _userManager.ChangePasswordAsync(user, request.Model.OldPassword, request.Model.NewPassword);
        if (!UpdatePasswordResult.Succeeded)
            return new GlobalResponseDTO { Message = string.Join(' ', UpdatePasswordResult.Errors.Select(x => x.Description)) };
        try
        {
            await _emailSender.SendEmailAsync(user.Email!, "Notification about your password updates",
                $"<h2>Your password has been updated <h3>in {DateTime.Now}<p>if you didn't do this updates , please notify us</p></h3></h2>");
        }
        catch (Exception)
        {
        }
        user.LastUpdateForPassword = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        return new GlobalResponseDTO { IsSuccess = true, Message = ErrorMessages.PasswordResetSuccessfully };
    }
}
