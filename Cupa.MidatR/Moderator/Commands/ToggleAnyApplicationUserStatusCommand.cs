namespace Cupa.MidatR.Moderator.Commands;
public sealed class ToggleAnyApplicationUserStatusCommand(string userid, string selectedUserId, ToggleRequestType type) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public string SelectedUserId { get; } = selectedUserId;
    public ToggleRequestType Type { get; } = type;
}