namespace Cupa.MidatR.Moderator.Queries
{
    public sealed class GetAllManagersForModeratorDashBoardQuery(string userid) : IRequest<ICollection<UserDataForModeratorDashBoardViewModelDTO>>
    {
        public string UserId { get; } = userid;
    }
}