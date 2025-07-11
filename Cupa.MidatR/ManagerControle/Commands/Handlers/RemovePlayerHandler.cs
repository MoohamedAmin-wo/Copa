namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
internal sealed class RemovePlayerHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<RemovePlayerCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<GlobalResponseDTO> Handle(RemovePlayerCommand request, CancellationToken cancellationToken)
    {
        var managerUser = await _userManager.FindByNameAsync(request.UserId);
        if (managerUser is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        var manager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(managerUser.Id));
        if (manager is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        var club = await _unitOfWork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
        if (club is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        var clubPlayer = await _unitOfWork.clubPlayer.FindSingleAsync(x => x.PlayerId.Equals(request.PlayerId));
        if (clubPlayer is null)
        {
            return new GlobalResponseDTO { Message = "Player not found !" };
        }

        var player = await _unitOfWork.player.FindSingleAsync(x => x.Id.Equals(clubPlayer.PlayerId));
        if (player is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        try
        {
            await _unitOfWork.clubPlayer.DeleteAsync(clubPlayer);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"unable to delete current record ! ,\n{ex.Message}" };
        }

        player.IsFree = true;
        player.UpdatedBy = managerUser.Id;
        player.UpdatedOn = DateTime.UtcNow;
        try
        {
            await _unitOfWork.player.UpdateAsync(player);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = ex.Message };
        }

        club.PlayersCount--;

        await _unitOfWork.CommitAsync();
        return new GlobalResponseDTO { IsSuccess = true, Message = "Deleted Successfully" };
    }
}