namespace Cupa.MidatR.Auth.Commands
{
    public class ResetPasswordAfterForgetCommand(ForgetPasswordModelDTO model) : IRequest<GlobalResponseDTO>
    {
        public ForgetPasswordModelDTO Model { get; } = model;
    }
}
