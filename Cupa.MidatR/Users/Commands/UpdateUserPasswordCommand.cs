namespace Cupa.MidatR.Users.Commands;
public sealed class UpdateUserPasswordCommand(string userid, UpdatePasswordModelDTO model) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public UpdatePasswordModelDTO Model { get; } = model;
}
