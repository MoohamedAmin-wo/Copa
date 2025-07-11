namespace Cupa.MidatR.Moderator.Queries;
public sealed class GetFreePlayersForModeratorDashboardQuery(string userid, StatusType status) : IRequest<IReadOnlyCollection<FreePlayerForModeratorDashBoardViewModelDTO>>
{
    public string UserId { get; } = userid;
    public StatusType Status { get; } = status;
}