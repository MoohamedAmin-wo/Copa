namespace Cupa.MidatR.Users.Commands;
public sealed class UpdateUserPhoneNumberCommand(string userid, string phonenumber) : IRequest<GlobalResponseDTO>
{
    public string Phonenumber { get; } = phonenumber;
    public string UserId { get; } = userid;
}
