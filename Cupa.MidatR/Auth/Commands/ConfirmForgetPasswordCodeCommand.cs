namespace Cupa.MidatR.Auth.Commands
{
    public sealed class ConfirmForgetPasswordCodeCommand(ConfirmCodeModelDTO model) : IRequest<GlobalResponseDTO>
    {
        public ConfirmCodeModelDTO Model { get; } = model;
    }
}
