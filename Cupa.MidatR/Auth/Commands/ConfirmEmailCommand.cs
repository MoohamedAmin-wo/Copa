namespace Cupa.MidatR.Auth.Commands;
public class ConfirmEmailCommand(ConfirmCodeModelDTO model) : IRequest<GlobalResponseDTO>
{
    public ConfirmCodeModelDTO Model { get; } = model;
}