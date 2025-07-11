namespace Cupa.MidatR.Dashboard.Queries;
public sealed class GetSelectedAdminForDashBoardQuery(string userid, int adminid) : IRequest<AdminViewForManagerModelDTO>
{
    public string UserId { get; set; } = userid;
    public int AdminId { get; set; } = adminid;
}

