namespace Cupa.MidatR.Users.Commands;
public sealed class UpdateUserFullnameCommand(string userid, UpdateNameModelDTO model) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public UpdateNameModelDTO Model { get; } = model;
}
