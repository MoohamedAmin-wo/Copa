namespace Cupa.MidatR.Dashboard.Queries;
public sealed class GetClubDetailsForManagerDashBoardViewQuery(string userid) : IRequest<ClubDataForDashBoardViewModelDTO>
{
    public string UserId { get; } = userid;
}