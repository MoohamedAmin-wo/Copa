using Cupa.MidatR.ManagerControle.Commands.DTOs;

namespace Cupa.MidatR.ManagerControle.Commands.Validators;

public sealed class CreateManagerValidator : AbstractValidator<ManagerModelDTO>
{
    public CreateManagerValidator()
    {
        RuleFor(x => x.StoryAbout).NotNull().NotEmpty().WithMessage("Story section can't be null or empty")
            .MaximumLength(5000).MinimumLength(5).Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters);

        RuleFor(x => x.StoryWithClub).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredField)
            .MaximumLength(5000).MinimumLength(5).Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters);
    }
}