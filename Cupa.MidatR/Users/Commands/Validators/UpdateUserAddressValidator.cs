namespace Cupa.MidatR.Users.Commands.Validators;

public class UpdateUserAddressValidator : AbstractValidator<UserAddressModelDTO>
{
    public UpdateUserAddressValidator()
    {
        RuleFor(x => x.City).NotEmpty().NotNull().WithMessage(ErrorMessages.RequiredField)
        .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
        .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
        .MaximumLength(256).MinimumLength(3);

        RuleFor(x => x.Country).NotEmpty().NotNull().WithMessage(ErrorMessages.RequiredField)
        .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
        .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
        .MaximumLength(256).MinimumLength(3);

        RuleFor(x => x.Governrate).NotEmpty().NotNull().WithMessage(ErrorMessages.RequiredField)
      .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
      .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
      .MaximumLength(256).MinimumLength(3);

        RuleFor(x => x.Street).NotEmpty().NotNull().WithMessage(ErrorMessages.RequiredField)
      .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
      .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
      .MaximumLength(256).MinimumLength(3);

        RuleFor(x => x.Regoin).NotEmpty().NotNull().WithMessage(ErrorMessages.RequiredField)
      .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
      .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
      .MaximumLength(256).MinimumLength(3);

    }
}
