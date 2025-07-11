namespace Cupa.MidatR.Auth.Commands;
public class RegisterRequestCommand(RegisterRequestModelDTO model) : IRequest<GlobalResponseDTO>
{
    public RegisterRequestModelDTO Model { get; } = model;
}