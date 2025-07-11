namespace Cupa.MidatR.Auth.Commands.Handlers;
internal sealed class RegisterRequestHandler(IAuthService authService) : IRequestHandler<RegisterRequestCommand, GlobalResponseDTO>
{
    private readonly IAuthService _authService = authService;
    public async Task<GlobalResponseDTO> Handle(RegisterRequestCommand request, CancellationToken cancellationToken)
    {
        var defaultUser = await _authService.GetUserByEmailAsync(request.Model.email);
        if (defaultUser != null && !await _authService.IsUserEmailConfirmedAsync(defaultUser))
            return new GlobalResponseDTO { Message = ErrorMessages.EmailIsRegisteredButNeedConfirmation };

        if (await _authService.CheckUserExistByUseremailAsync(request.Model.email))
            return new GlobalResponseDTO { Message = ErrorMessages.EmailIsTaken };

        if (await _authService.CheckUserExistByUseremailAsync(request.Model.Username))
            return new GlobalResponseDTO { Message = ErrorMessages.UserNameIsTaken };

        var user = new ApplicationUser
        {
            FirstName = request.Model.FirstName,
            LastName = request.Model.LastName,
            UserName = request.Model.Username,
            Email = request.Model.email,
            BirthDate = request.Model.BirthDate,
            ProfilePictureUrl = CupaDefaults.DefaultProfilePicture,
            ProfilePictureThumbnailUrl = CupaDefaults.DefaultProfilePictureThumbnail,
            ProfilePictureUid = null
        };

        if (user.Age < 7)
            return new GlobalResponseDTO { Message = "allowed age to join , start from 7 years old !" };

        var creationResult = await _authService.CreateNewUserAsync(user, request.Model.password);
        if (!string.IsNullOrEmpty(creationResult))
            return new GlobalResponseDTO { Message = creationResult };

        var code = await _authService.GenerateNewConfirmationCodeAsync(user);

        var emailSendResult = await _authService.SendEmailToUserAsync(user.Email, "Confirm your email",
            $"<h2>your confirmation Code is : {code}<p>this code is valid for 2 minutes</p></h2>");

        if (!string.IsNullOrEmpty(emailSendResult))
            return new GlobalResponseDTO { Message = emailSendResult };

        return new GlobalResponseDTO { IsSuccess = true, Message = ErrorMessages.RegisterCompleteSuccessfully };
    }
}