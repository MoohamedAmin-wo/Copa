namespace Cupa.MidatR.ManagerControle.Queries;
public sealed class GetActiveAdminsQuery(string userid) : IRequest<IReadOnlyCollection<AdminQueryModelDTO>> { public string UserId { get; } = userid; }