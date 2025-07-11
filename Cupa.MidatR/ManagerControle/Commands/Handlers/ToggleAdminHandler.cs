namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
internal sealed class ToggleAdminHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<ToggleAdminCommand, GlobalResponseDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<GlobalResponseDTO> Handle(ToggleAdminCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _userManager.IsInRoleAsync(currentUser, CupaRoles.Manager))
            return new GlobalResponseDTO { Message = ErrorMessages.UnAuthorizedUser };

        var admin = await _unitOfWork.admins.FindSingleAsync(x => x.Id.Equals(request.AdminId));
        if (admin is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        var user = await _userManager.FindByIdAsync(admin.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        user.IsDeleted = !user.IsDeleted;
        user.UpdatedOn = DateTime.UtcNow;
        user.UpdatedBy = request.UserId;

        try
        {
            await _userManager.UpdateAsync(user);
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };
        }

        await _unitOfWork.CommitAsync();
        string status = user.IsDeleted ? "Deleted" : "Active";
        return new GlobalResponseDTO { IsSuccess = true, Message = status };
    }
}
