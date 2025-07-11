namespace Cupa.MidatR.ManagerControle.Queries;
public sealed class GetBinnedClubPlayersQuery(string userid) : IRequest<IReadOnlyCollection<ClubPlayerQueryModelDTO>> { public string UserId { get; } = userid; }
