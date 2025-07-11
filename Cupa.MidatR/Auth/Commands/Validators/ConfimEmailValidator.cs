namespace Cupa.MidatR.Auth.Commands.Validators;
public class ConfimEmailValidator : AbstractValidator<ConfirmCodeModelDTO>
{
    public ConfimEmailValidator()
    {
        RuleFor(x => x.Code).NotEmpty().NotNull().WithMessage("Code can't be null or empty !")
            .Matches(RegexPatterns.ConfirmationCode).WithMessage("Confirmation code isn't valid , only numbers allowed !")
            .MaximumLength(6).WithMessage("Confirmation code can't be more than 6!")
            .MinimumLength(6).WithMessage("Confirmation code must be 6 digits at least !");

        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email can't be null or empty !")
            .Matches(RegexPatterns.EmailAddress).WithMessage(ErrorMessages.InValidEmail)
            .MaximumLength(256).WithMessage("Email can't be more than 256 chr.")
            .MinimumLength(10).WithMessage("email must be 10 chr. at least !");
    }
}