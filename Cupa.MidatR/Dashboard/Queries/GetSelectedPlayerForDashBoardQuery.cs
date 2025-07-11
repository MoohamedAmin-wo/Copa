namespace Cupa.MidatR.Dashboard.Queries;
public sealed class GetSelectedPlayerForDashBoardQuery(string userid, int playerid) : IRequest<PlayerViewForManagerModelDTO>
{
    public string UserId { get; set; } = userid;
    public int PlayerId { get; set; } = playerid;
}

