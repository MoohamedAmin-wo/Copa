namespace Cupa.MidatR.Users.Queries.Handlers;
internal sealed class GetTopRatedFreePlayersForHomePageViewQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTopRatedFreePlayersForHomePageViewQuery, IReadOnlyCollection<PlayersForHomePageQueryModelDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IReadOnlyCollection<PlayersForHomePageQueryModelDTO>> Handle(GetTopRatedFreePlayersForHomePageViewQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Player> players;

        if (request.ForHome)
        {
            players = await _unitOfWork.player.GetAllAsync(includes: x => x.Include(u => u.User).Include(p => p.Position), predicate: x => !x.IsDeleted && !x.IsBinned && x.IsFree, take: 10);
        }
        else
        {
            players = await _unitOfWork.player.GetAllAsync(includes: x => x.Include(u => u.User).Include(p => p.Position), predicate: x => !x.IsDeleted && !x.IsBinned && x.IsFree, take: 0);
        }

        var model = players.Select(x => new PlayersForHomePageQueryModelDTO
        {
            Id = x.Id,
            Age = x.User.Age,
            Fullname = string.Concat(x.User.FirstName, ' ', x.User.LastName),
            Nickname = x.NickName,
            Position = x.Position.PositionAPPR,
            ProfilePictureUrl = x.ProfilePictureUrl,
            Views = x.Views
        });

        return [.. model];
    }
}