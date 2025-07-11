namespace Cupa.MidatR.Users.Queries;
public sealed class GetCurrentUserDataQuery(string userid) : IRequest<GetCurrentUserDataModelDTO> { public string UserId { get; } = userid; }