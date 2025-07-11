namespace Cupa.MidatR.ManagerControle.Queries;
public sealed class GetActiveClubPlayersQuery(string userid) : IRequest<IReadOnlyCollection<ClubPlayerQueryModelDTO>> { public string UserId { get; } = userid; }
