using Cupa.MidatR.ManagerControle.Commands.DTOs;

namespace Cupa.MidatR.ManagerControle.Commands.Validators;
public sealed class CreateClubPlayerValidator : AbstractValidator<PlayerModelDTO>
{
    public CreateClubPlayerValidator()
    {
        RuleFor(x => x.PositionId).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredField)
            .Matches(RegexPatterns.AllowedPosition).WithMessage("invalid position selected ");
        
        RuleFor(x => x.Number).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredField)
            .GreaterThanOrEqualTo(1).LessThanOrEqualTo(99);

        RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredField)
            .Matches(RegexPatterns.MobileNumber).WithMessage(ErrorMessages.InValidPhonenumber);
        
        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email can't be null or empty !")
          .Matches(RegexPatterns.EmailAddress).WithMessage(ErrorMessages.InValidEmail)
          .MaximumLength(256).WithMessage("Email can't be more than 256 chr.")
          .MinimumLength(6).WithMessage("email must be 10 chr. at least !");

        RuleFor(x => x.Nickname).NotEmpty().NotNull().WithMessage(ErrorMessages.RequiredField)
           .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
           .MaximumLength(128).WithMessage("Username can't be more than 128 chr.")
           .MinimumLength(6).WithMessage("Username must be 6 chr. at least !");

        RuleFor(x => x.Firstname).NotEmpty().NotNull().WithMessage(ErrorMessages.RequiredField)
          .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
          .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
          .MaximumLength(64).WithMessage("FirstName can't be more than 64 chr.")
          .MinimumLength(3).WithMessage("FirstName must be 3 chr. at least !");

        RuleFor(x => x.Lastname).NotEmpty().NotNull().WithMessage(ErrorMessages.RequiredField)
          .Matches(RegexPatterns.DenySpecialCharacters).WithMessage(ErrorMessages.DenySpecialCharacters)
          .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
          .MaximumLength(128).WithMessage("LastName can't be more than 128 chr.")
          .MinimumLength(3).WithMessage("LastName must be 3 chr. at least !");
    }
}