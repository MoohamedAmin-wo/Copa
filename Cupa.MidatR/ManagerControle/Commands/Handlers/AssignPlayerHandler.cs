using Cupa.Application.Services.ClubServices;

namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
internal sealed class AssignPlayerHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IFilesServices filesServices, IClubManagmentServices clubManagmentServices) : IRequestHandler<AssignPlayerCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClubManagmentServices _clubManagmentServices = clubManagmentServices;

    public async Task<GlobalResponseDTO> Handle(AssignPlayerCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        var club = await _clubManagmentServices.GetCurrentClubAsync(user);

        if (club is null)
            return new GlobalResponseDTO { Message = "No club was define !" };

        var player = await _unitOfWork.player.FindSingleAsync(x => x.Id.Equals(request.PlayerId));
        if (player is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        var clubPlayer = new ClubPlayer
        {
            ClubId = club.Id,
            IsAvaliableForSale = false,
            PlayerId = player.Id,
            HasCurrentContract = true
        };

        player.IsFree = false;
        player.IsBinned = false;
        player.UpdatedBy = user.Id;
        player.UpdatedOn = DateTime.UtcNow;

        var transaction = _unitOfWork.BeginTransactionAsync();

        try
        {
            await _unitOfWork.player.UpdateAsync(player);
            transaction.CreateSavepoint("update player entity");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            return new GlobalResponseDTO { Message = "unable to update this record right now !" };
        }

        try
        {
            await _unitOfWork.clubPlayer.CreateAsync(clubPlayer);
            transaction.CreateSavepoint("add club player ");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            return new GlobalResponseDTO { Message = "unable to create new record right now !" };
        }

        club.PlayersCount++;
        await transaction.CommitAsync(cancellationToken);
        await _unitOfWork.CommitAsync();

        return new GlobalResponseDTO { IsSuccess = true, Message = "congratulations for your new player " };
    }
}
