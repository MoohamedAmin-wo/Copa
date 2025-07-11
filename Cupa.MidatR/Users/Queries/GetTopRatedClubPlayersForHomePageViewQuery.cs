namespace Cupa.MidatR.Users.Queries;
public sealed class GetTopRatedClubPlayersForHomePageViewQuery(bool forHome = true) : IRequest<IReadOnlyCollection<PlayersForHomePageQueryModelDTO>>
{
    public bool ForHome { get; } = forHome;
}