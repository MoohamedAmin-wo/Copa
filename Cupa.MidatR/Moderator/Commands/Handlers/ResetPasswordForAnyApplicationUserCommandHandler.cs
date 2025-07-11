namespace Cupa.MidatR.Moderator.Commands.Handlers;
internal sealed class ResetPasswordForAnyApplicationUserCommandHandler(UserManager<ApplicationUser> userManager, IEmailSender emailSender) : IRequestHandler<ResetPasswordForAnyApplicationUserCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task<GlobalResponseDTO> Handle(ResetPasswordForAnyApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _userManager.IsInRoleAsync(currentUser, CupaRoles.Moderator))
            return new GlobalResponseDTO { Message = ErrorMessages.UnAuthorizedUser };

        var appUser = await _userManager.FindByIdAsync(request.SelectedUserId);
        if (appUser is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        try
        {
            await _userManager.RemovePasswordAsync(appUser);

            try
            {
                await _userManager.AddPasswordAsync(appUser, CupaDefaults.DefaultPassword);
            }
            catch (Exception)
            {
                return new GlobalResponseDTO { Message = "unable to reset user password" };
            }
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"unable to reset user password , Exception Details : {ex.Message} " };
        }

        await _emailSender.SendEmailAsync(appUser.Email, "Notification about your password updates",
               $"<h2>Your password has been reseted Successfully  <h3>at {DateTime.UtcNow}<p>your new password is {CupaDefaults.DefaultPassword}</p></h3></h2>");

        return new GlobalResponseDTO { IsSuccess = true, Message = "new Password sent  to User email " };
    }
}
