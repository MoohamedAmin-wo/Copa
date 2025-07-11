namespace Cupa.MidatR.Auth.Commands.Handlers;
internal sealed class ContactUsFromUnAuthentacatedUsersCommandHandler(IEmailSender emailSender) : IRequestHandler<ContactUsFromUnAuthentacatedUsersCommand, GlobalResponseDTO>
{

    private readonly IEmailSender _emailSender = emailSender;

    public async Task<GlobalResponseDTO> Handle(ContactUsFromUnAuthentacatedUsersCommand request, CancellationToken cancellationToken)
    {
        // validate the model ...

        try
        {
            await _emailSender.SendEmailAsync(CupaDefaults.DefaultEmail, $"From : {request.Model.Email}",
            $"<h2>Message from {request.Model.Fullname}<h3><p>{request.Model.Subject}</p></h3></h2>");
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = "Faild to send email !" };
        }

        return new GlobalResponseDTO { IsSuccess = true, Message = "Email send Successfully " };
    }
}
