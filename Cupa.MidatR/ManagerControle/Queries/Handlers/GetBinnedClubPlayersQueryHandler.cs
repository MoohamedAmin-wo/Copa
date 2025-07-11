namespace Cupa.MidatR.ManagerControle.Queries.Handlers;
sealed class GetBinnedClubPlayersQueryHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<GetBinnedClubPlayersQuery, IReadOnlyCollection<ClubPlayerQueryModelDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<IReadOnlyCollection<ClubPlayerQueryModelDTO>> Handle(GetBinnedClubPlayersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null) { return null!; }

        var manager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
        if (manager is null) { return null!; }

        var club = await _unitOfWork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
        if (club is null) { return null!; }


        var binnedClubPlayers = await _unitOfWork.player.GetAllAsync(includes: x => x.Include(b => b.User), predicate: x => x.IsBinned, take: 0);

        ICollection<ClubPlayerQueryModelDTO> returnModel = null;

        return [.. returnModel!];

    }
}
