namespace Cupa.MidatR.Users.Queries.Handlers;
internal sealed class GetTopRatedClubPlayersForHomePageViewQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTopRatedClubPlayersForHomePageViewQuery, IReadOnlyCollection<PlayersForHomePageQueryModelDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IReadOnlyCollection<PlayersForHomePageQueryModelDTO>> Handle(GetTopRatedClubPlayersForHomePageViewQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<ClubPlayer> players;

        if (request.ForHome)
        {
            players = await _unitOfWork.clubPlayer.GetAllAsync(
                includes: x => x.Include(p => p.Player).Include(p => p.Player.Position).Include(u => u.Player).ThenInclude(o => o.User),
                predicate: x => !x.Player.IsDeleted && !x.Player.IsBinned, take: 10);
        }
        else
        {
            players = await _unitOfWork.clubPlayer.GetAllAsync(
                includes: x => x.Include(p => p.Player).Include(p => p.Player.Position).Include(u => u.Player).ThenInclude(o => o.User),
                predicate: x => !x.Player.IsDeleted && !x.Player.IsBinned, take: 0);
        }

        var model = players.Select(x => new PlayersForHomePageQueryModelDTO
        {
            Id = x.Player.Id,
            Age = x.Player.User.Age,
            Fullname = string.Concat(x.Player.User.FirstName, ' ', x.Player.User.LastName),
            Nickname = x.Player.NickName,
            Position = x.Player.Position.PositionAPPR,
            ProfilePictureUrl = x.Player.ProfilePictureUrl,
            Views = x.Player.Views
        });

        return [.. model];
    }
}
