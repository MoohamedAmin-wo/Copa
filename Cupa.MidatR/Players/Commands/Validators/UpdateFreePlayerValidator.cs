using Cupa.MidatR.Players.Commands.DTOs;

namespace Cupa.MidatR.Players.Commands.Validators;

public class UpdateFreePlayerValidator : AbstractValidator<FreePlayerModelDTO>
{
    public UpdateFreePlayerValidator()
    {
        RuleFor(x => x.PositionId).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredField)
           .Matches(RegexPatterns.AllowedPosition).WithMessage("invalid position selected ");

        RuleFor(x => x.NickName).NotEmpty().NotNull().WithMessage(ErrorMessages.RequiredField)
           .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
           .MaximumLength(128).WithMessage("Username can't be more than 128 chr.")
           .MinimumLength(6).WithMessage("Username must be 6 chr. at least !");

        RuleFor(x => x.StoryAbout).NotNull().NotEmpty().WithMessage("Story section can't be null or empty")
            .MaximumLength(5000).MinimumLength(5).Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters);

        RuleFor(x => x.PreefAbout).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredField)
            .MaximumLength(5000).MinimumLength(5).Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters);
    }
}
