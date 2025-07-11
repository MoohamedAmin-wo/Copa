namespace Cupa.MidatR.Users.Queries;
public sealed class UserProfileViewQuery(string userid) : IRequest<UserProfileModelDTO> { public string UserId { get; } = userid; }