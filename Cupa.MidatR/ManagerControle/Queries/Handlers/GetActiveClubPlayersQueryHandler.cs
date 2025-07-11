namespace Cupa.MidatR.ManagerControle.Queries.Handlers;
sealed class GetActiveClubPlayersQueryHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<GetActiveClubPlayersQuery, IReadOnlyCollection<ClubPlayerQueryModelDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<IReadOnlyCollection<ClubPlayerQueryModelDTO>> Handle(GetActiveClubPlayersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null) { return null!; }

        var manager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
        if (manager is null) { return null!; }

        var club = await _unitOfWork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
        if (club is null) { return null!; }

        var clubPlayers = await _unitOfWork.player.GetAllAsync(
            includes: u => u.Include(x => x.User).Include(p => p.Position),
            predicate: c => !c.IsBinned && !c.IsDeleted,
            take: 0);

        var returnModel = clubPlayers.Select(
            x => new ClubPlayerQueryModelDTO
            {
                Id = x.Id,
                Fullname = string.Concat(x.User.FirstName, " ", x.User.LastName),
                Email = x.User.Email,
                Phone = x.User.PhoneNumber,
                PictureUrl = x.User.ProfilePictureUrl ?? CupaDefaults.DefaultProfilePicture,
                Age = x.User.Age,
                Position = x.Position.PositionName,
                Status = x.IsDeleted ? "Deleted" : "Active"
            });

        return [.. returnModel];
    }
}
