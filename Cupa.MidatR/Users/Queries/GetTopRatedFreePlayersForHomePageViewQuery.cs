namespace Cupa.MidatR.Users.Queries;
public sealed class GetTopRatedFreePlayersForHomePageViewQuery(bool forHome = true) : IRequest<IReadOnlyCollection<PlayersForHomePageQueryModelDTO>>
{
    public bool ForHome { get; } = forHome;
}