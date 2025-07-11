namespace Cupa.MidatR.Users.Commands.Validators;
public class UpdateUserFullnameValidator : AbstractValidator<UpdateNameModelDTO>
{
    public UpdateUserFullnameValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty().NotNull().WithMessage("FirstName can't be null or empty !")
        .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
        .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
        .MaximumLength(64).WithMessage("FirstName can't be more than 64 chr.")
        .MinimumLength(3).WithMessage("FirstName must be 3 chr. at least !");

        RuleFor(x => x.Lastname).NotEmpty().NotNull().WithMessage("LastName can't be null or empty !")
          .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
          .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
          .MaximumLength(128).WithMessage("LastName can't be more than 128 chr.")
          .MinimumLength(3).WithMessage("LastName must be 3 chr. at least !");
    }
}
