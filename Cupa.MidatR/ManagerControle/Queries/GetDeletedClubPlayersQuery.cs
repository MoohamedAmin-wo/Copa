namespace Cupa.MidatR.ManagerControle.Queries;
public sealed class GetDeletedClubPlayersQuery(string userid) : IRequest<IReadOnlyCollection<ClubPlayerQueryModelDTO>> { public string UserId { get; } = userid; }
