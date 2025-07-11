namespace Cupa.MidatR.ManagerControle.Queries.Handlers;
sealed class GetFreeClubPlayersQueryHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<GetFreeClubPlayersQuery, IReadOnlyCollection<ClubPlayerQueryModelDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<IReadOnlyCollection<ClubPlayerQueryModelDTO>> Handle(GetFreeClubPlayersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null) { return null!; }

        var manager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
        if (manager is null) { return null!; }

        var club = await _unitOfWork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
        if (club is null) { return null!; }

        var clubPlayers = await _unitOfWork.player.GetAllAsync(includes: u => u.Include(x => x.User), predicate: c => c.IsFree && !c.IsDeleted, take: 0);

        ICollection<ClubPlayerQueryModelDTO> returnModel = null;

        return [.. returnModel];
    }
}
