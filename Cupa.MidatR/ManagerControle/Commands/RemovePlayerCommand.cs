namespace Cupa.MidatR.ManagerControle.Commands;
public sealed class RemovePlayerCommand(int playerid, string userid) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public int PlayerId { get; } = playerid;
}