namespace Cupa.MidatR.Moderator.Commands.Handlers;
internal sealed class ToggleAnyApplicationUserStatusCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<ToggleAnyApplicationUserStatusCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<GlobalResponseDTO> Handle(ToggleAnyApplicationUserStatusCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _userManager.IsInRoleAsync(currentUser, CupaRoles.Moderator))
            return new GlobalResponseDTO { Message = ErrorMessages.UnAuthorizedUser };

        var ExistUser = await _userManager.FindByIdAsync(request.SelectedUserId);
        if (ExistUser is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        if (request.Type.Equals(ToggleRequestType.Deleted))
        {
            ExistUser.IsDeleted = !ExistUser.IsDeleted;
        }

        if (request.Type.Equals(ToggleRequestType.Block))
        {
            ExistUser.IsBlocked = !ExistUser.IsBlocked;
        }

        ExistUser.UpdatedOn = DateTime.UtcNow;
        ExistUser.UpdatedBy = currentUser.Id;

        try
        {
            await _userManager.UpdateAsync(ExistUser);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"Unable to toggle the current user ! ,\nException details : {ex.Message}" };
        }

        return new GlobalResponseDTO { IsSuccess = true, Message = "toggle successed " };
    }
}
