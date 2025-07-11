namespace Cupa.MidatR.Dashboard.Queries;
public sealed class GetClubPlayersForDashBoardViewQuery(string userid, StatusType status = StatusType.Active) : IRequest<ICollection<ClubPlayerForDashBoardViewModelDTO>>
{
    public string UserId { get; } = userid;
    public StatusType Status { get; } = status;
}
