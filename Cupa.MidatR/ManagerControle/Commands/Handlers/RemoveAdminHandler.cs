namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
internal sealed class RemoveAdminHandler : IRequestHandler<RemoveAdminCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _uintOfwork;

    public RemoveAdminHandler(UserManager<ApplicationUser> userManager, IUnitOfWork uintOfwork)
    {
        _userManager = userManager;
        _uintOfwork = uintOfwork;
    }

    public async Task<GlobalResponseDTO> Handle(RemoveAdminCommand request, CancellationToken cancellationToken)
    {
        var manager = await _userManager.FindByNameAsync(request.UserId);
        if (manager is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _userManager.IsInRoleAsync(manager, CupaRoles.Manager))
            return new GlobalResponseDTO { Message = "You are not allowed to take this action " };

        var admin = await _uintOfwork.admins.FindSingleAsync(x => x.Id.Equals(request.AdminId));
        if (admin is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        var user = await _userManager.FindByIdAsync(admin.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _userManager.IsInRoleAsync(user, CupaRoles.Admin))
            return new GlobalResponseDTO { Message = ErrorMessages.UnExistedRoleOrUser };

        try
        {
            await _uintOfwork.admins.DeleteAsync(admin);
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };
        }

        try
        {
            await _userManager.RemoveFromRoleAsync(user, CupaRoles.Admin);
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };
        }


        var club = await _uintOfwork.clubs.FindSingleAsync(x => x.Id.Equals(admin.ClubId));
        if (club is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        club.AdminsCount--;

        await _uintOfwork.CommitAsync();

        return new GlobalResponseDTO { IsSuccess = true, Message = "Remove Successfully Compelete" };
    }
}
