namespace Cupa.MidatR.Auth.Commands.Validators;

public class TokeRequestValidator : AbstractValidator<TokenRequestModelDTO>
{
    public TokeRequestValidator()
    {
        RuleFor(x => x.email).NotEmpty().NotNull().WithMessage("Email can't be null or empty !")
       .Matches(RegexPatterns.EmailAddress).WithMessage(ErrorMessages.InValidEmail);

        RuleFor(x => x.password).NotEmpty().NotNull().WithMessage("password can't be null or empty !");
    }
}