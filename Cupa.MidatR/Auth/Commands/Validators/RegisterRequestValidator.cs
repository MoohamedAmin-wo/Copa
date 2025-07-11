namespace Cupa.MidatR.Auth.Commands.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestModelDTO>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.email).NotEmpty().NotNull().WithMessage("Email can't be null or empty !")
           .Matches(RegexPatterns.EmailAddress).WithMessage(ErrorMessages.InValidEmail)
           .MaximumLength(256).WithMessage("Email can't be more than 256 chr.")
           .MinimumLength(6).WithMessage("email must be 10 chr. at least !");

        RuleFor(x => x.Username).NotEmpty().NotNull().WithMessage("Username can't be null or empty !")
           .Matches(RegexPatterns.Username).WithMessage(ErrorMessages.InvalidUsername)
           .MaximumLength(128).WithMessage("Username can't be more than 128 chr.")
           .MinimumLength(6).WithMessage("Username must be 6 chr. at least !");

        RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("FirstName can't be null or empty !")
          .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
          .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
          .MaximumLength(64).WithMessage("FirstName can't be more than 64 chr.")
          .MinimumLength(3).WithMessage("FirstName must be 3 chr. at least !");

        RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("LastName can't be null or empty !")
          .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
          .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
          .MaximumLength(128).WithMessage("LastName can't be more than 128 chr.")
          .MinimumLength(3).WithMessage("LastName must be 3 chr. at least !");

        RuleFor(x => x.password).NotEmpty().NotNull().WithMessage("password can't be null or empty !")
         .Matches(RegexPatterns.Password).WithMessage(ErrorMessages.WeakPassword)
         .MaximumLength(128).WithMessage("password can't be more than 128 chr.")
         .MinimumLength(8).WithMessage("password must be 8 chr. at least !");
    }
}