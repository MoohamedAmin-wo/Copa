namespace Cupa.MidatR.Users.Commands.Validators;

public class UpdateUserPasswordValidator : AbstractValidator<UpdatePasswordModelDTO>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(x => x.NewPassword).NotEmpty().NotNull().WithMessage("password can't be null or empty !")
         .Matches(RegexPatterns.Password).WithMessage(ErrorMessages.WeakPassword)
         .MaximumLength(128).WithMessage("password can't be more than 128 chr.")
         .MinimumLength(8).WithMessage("password must be 8 chr. at least !");

        RuleFor(x => x.ConfirmationPassword).NotEmpty().NotNull().WithMessage("Confirmation password can't be null or empty !")
        .Equal(x => x.NewPassword).WithMessage(ErrorMessages.ConfirmPasswordNotMatch);
    }
}
