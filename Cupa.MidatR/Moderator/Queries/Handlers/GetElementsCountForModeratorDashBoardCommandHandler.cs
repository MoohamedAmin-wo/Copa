namespace Cupa.MidatR.Moderator.Queries.Handlers;
internal sealed class GetElementsCountForModeratorDashBoardCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<GetElementsCountForModeratorDashBoardCommand, ElementsCountForModeratorDashBoardViewModelDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<ElementsCountForModeratorDashBoardViewModelDTO> Handle(GetElementsCountForModeratorDashBoardCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return null!;

        if (!await _userManager.IsInRoleAsync(currentUser, CupaRoles.Moderator))
            return null!;

        var usersCount = await _unitOfWork.users.GetCountAsync();
        var adminsCount = await _unitOfWork.admins.GetCountAsync();
        var managersCount = await _unitOfWork.managers.GetCountAsync();
        var freePlayersCount = await _unitOfWork.player.GetCountAsync(x => x.IsFree);
        var clubPlayersCount = await _unitOfWork.clubPlayer.GetCountAsync();


        return new ElementsCountForModeratorDashBoardViewModelDTO
        {
            AdminsCount = adminsCount,
            ManagersCount = managersCount,
            UsersCount = usersCount,
            ClubPlayersCount = clubPlayersCount,
            FreePlayersCount = freePlayersCount,
        };
    }
}
