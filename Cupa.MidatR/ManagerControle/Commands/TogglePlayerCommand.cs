namespace Cupa.MidatR.ManagerControle.Commands
{
    public sealed class TogglePlayerCommand(string userid, int playerid) : IRequest<GlobalResponseDTO>
    {
        public string UserId { get; } = userid;
        public int PlayerId { get; } = playerid;
    }
}
