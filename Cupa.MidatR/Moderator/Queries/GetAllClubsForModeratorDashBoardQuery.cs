namespace Cupa.MidatR.Moderator.Queries;
public sealed class GetAllClubsForModeratorDashBoardQuery(string userid) : IRequest<IReadOnlyCollection<ClubDataForModeratorDashBoardViewModelDTO>>
{
    public string UserId { get; } = userid;
}
