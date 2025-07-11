namespace Cupa.MidatR.ManagerControle.Commands
{
    public class ToggleAdminCommand(int adminid, string userId) : IRequest<GlobalResponseDTO>
    {
        public int AdminId { get; } = adminid;
        public string UserId { get; } = userId;
    }
}
