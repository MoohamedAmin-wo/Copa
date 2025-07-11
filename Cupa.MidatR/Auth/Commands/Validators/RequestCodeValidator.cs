namespace Cupa.MidatR.Auth.Commands.Validators;

public class RequestCodeValidator : AbstractValidator<RequestCodeModelDTO>
{
    public RequestCodeValidator()
    {
        RuleFor(x => x.email).NotEmpty().NotNull().WithMessage("Email can't be null or empty !")
       .Matches(RegexPatterns.EmailAddress).WithMessage(ErrorMessages.InValidEmail);
    }
}
