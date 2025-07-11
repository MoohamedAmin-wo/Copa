using Cupa.MidatR.ManagerControle.Commands.DTOs;

namespace Cupa.MidatR.ManagerControle.Commands.Validators;

public sealed class CreateClubValidator : AbstractValidator<ClubModelDTO>
{
    public CreateClubValidator()
    {
        RuleFor(x => x.clubName).NotNull().NotEmpty().WithMessage("Club name can't be null or empty")
            .MaximumLength(128).MinimumLength(5).Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters);

        RuleFor(x => x.story).NotNull().NotEmpty().WithMessage("Story section can't be null or empty")
            .MaximumLength(5000).MinimumLength(5).Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters);

        RuleFor(x => x.about).NotNull().NotEmpty().WithMessage("About section can't be null or empty")
            .MaximumLength(1024).MinimumLength(5).Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters);
    }
}
