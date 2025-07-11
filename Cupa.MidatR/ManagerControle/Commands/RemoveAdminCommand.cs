namespace Cupa.MidatR.ManagerControle.Commands
{
    public sealed class RemoveAdminCommand(string userid, int adminid) : IRequest<GlobalResponseDTO>
    {
        public int AdminId { get; } = adminid;
        public string UserId { get; } = userid;
    }
}
