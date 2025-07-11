namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
class TogglePlayerHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<TogglePlayerCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<GlobalResponseDTO> Handle(TogglePlayerCommand req, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(req.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        var player = await _unitOfWork.player.FindSingleAsync(x => x.Id.Equals(req.PlayerId));
        if (player is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        player.IsDeleted = !player.IsDeleted;
        player.UpdatedBy = user.Id;
        player.UpdatedOn = DateTime.UtcNow;

        try
        {
            await _unitOfWork.player.UpdateAsync(player);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"can't update this player 's status right now .\nException Details : {ex.Message}" };
        }

        await _unitOfWork.CommitAsync();

        string status = player.IsDeleted ? "Deleted" : "Active";
        return new GlobalResponseDTO { IsSuccess = true, Message = status };
    }
}