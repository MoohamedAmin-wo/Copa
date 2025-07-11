namespace Cupa.MidatR.Auth.Commands.Validators;

public class ContactUsValidator : AbstractValidator<ContactUsModelDTO>
{
    public ContactUsValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email can't be null or empty !")
            .Matches(RegexPatterns.EmailAddress).WithMessage(ErrorMessages.InValidEmail)
            .MaximumLength(256).WithMessage("Email can't be more than 256 chr.")
            .MinimumLength(6).WithMessage("email must be 10 chr. at least !");

        RuleFor(x => x.Fullname).NotEmpty().NotNull().WithMessage("name can't be null or empty !")
            .Matches(RegexPatterns.CharactersOnly_Eng).WithMessage(ErrorMessages.OnlyEnglishLetters)
            .MaximumLength(256).WithMessage("name can't be more than 256 chr.")
            .MinimumLength(6).WithMessage("name must be 6 chr. at least !");

        RuleFor(x => x.Subject).NotEmpty().NotNull().WithMessage("subject can't be null or empty !")
            .Matches(RegexPatterns.NumbersAndChrOnly_ArEng).WithMessage("english and arabic chr. only allowed")
            .MaximumLength(1024).WithMessage("subject can't be more than 1024 chr.")
            .MinimumLength(10).WithMessage("subject must be 10 chr. at least !");
    }
}
