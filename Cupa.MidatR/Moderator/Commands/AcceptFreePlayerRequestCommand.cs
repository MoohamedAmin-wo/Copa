namespace Cupa.MidatR.Moderator.Commands;
public sealed class AcceptFreePlayerRequestCommand(int playerid, string userid, ApprovalType type) : IRequest<GlobalResponseDTO>
{
    public string UserID { get; } = userid;
    public int PlayerId { get; } = playerid;
    public ApprovalType Type { get; } = type;
}
