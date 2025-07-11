namespace Cupa.MidatR.Auth.Commands;
public sealed class ContactUsFromUnAuthentacatedUsersCommand(ContactUsModelDTO model) : IRequest<GlobalResponseDTO>
{
    public ContactUsModelDTO Model { get; } = model;
}
