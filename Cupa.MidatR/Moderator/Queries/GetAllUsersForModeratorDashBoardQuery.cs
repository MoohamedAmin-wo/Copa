namespace Cupa.MidatR.Moderator.Queries;
public sealed class GetAllUsersForModeratorDashBoardQuery(string userid, StatusType type) : IRequest<IReadOnlyCollection<UserDataForModeratorDashBoardViewModelDTO>>
{
    public string UserId { get; } = userid;
    public StatusType Type { get; } = type;
}