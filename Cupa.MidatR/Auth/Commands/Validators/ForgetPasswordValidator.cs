namespace Cupa.MidatR.Auth.Commands.Validators;

public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordModelDTO>
{
    public ForgetPasswordValidator()
    {
        RuleFor(x => x.email).NotEmpty().NotNull().WithMessage("Email can't be null or empty !")
          .Matches(RegexPatterns.EmailAddress).WithMessage(ErrorMessages.InValidEmail);

        RuleFor(x => x.newPassword).NotEmpty().NotNull().WithMessage("password can't be null or empty !")
         .Matches(RegexPatterns.Password).WithMessage(ErrorMessages.WeakPassword)
         .MaximumLength(128).WithMessage("password can't be more than 128 chr.")
         .MinimumLength(8).WithMessage("password must be 8 chr. at least !");

        RuleFor(x => x.confirmationPassword).NotEmpty().NotNull().WithMessage("Confirmation password can't be null or empty !")
        .Equal(x => x.newPassword).WithMessage(ErrorMessages.ConfirmPasswordNotMatch);
    }
}