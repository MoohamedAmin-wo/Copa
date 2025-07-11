namespace Cupa.MidatR.Moderator.Queries
{
    public sealed class GetAllAdminsForModeratorDashBoardQuery(string userid) : IRequest<ICollection<UserDataForModeratorDashBoardViewModelDTO>>
    {
        public string UserId { get; } = userid;
    }
}
