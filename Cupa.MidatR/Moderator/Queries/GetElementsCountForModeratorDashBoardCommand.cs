namespace Cupa.MidatR.Moderator.Queries;
public sealed class GetElementsCountForModeratorDashBoardCommand(string userid) : IRequest<ElementsCountForModeratorDashBoardViewModelDTO>
{
    public string UserId { get; set; } = userid;
}