namespace Cupa.MidatR.Users.Queries;
public sealed class GetSelectedPlayerFromUserQuery(string userid, int playerid) : IRequest<SelectedPlayerObjectForAuthointicatedUsersModelDTO>
{
    public string UserId { get; } = userid;
    public int PlayerId { get; } = playerid;
}
