namespace Cupa.MidatR.Users.Commands;
public sealed class UpdateUserAddressCommand(string userid, UserAddressModelDTO model) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public UserAddressModelDTO Model { get; } = model;
}