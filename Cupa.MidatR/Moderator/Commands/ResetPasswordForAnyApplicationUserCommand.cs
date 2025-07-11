namespace Cupa.MidatR.Moderator.Commands;
public sealed class ResetPasswordForAnyApplicationUserCommand(string selecteduserid, string userid) : IRequest<GlobalResponseDTO>
{
    public string SelectedUserId { get; set; } = selecteduserid;
    public string UserId { get; set; } = userid;
}
