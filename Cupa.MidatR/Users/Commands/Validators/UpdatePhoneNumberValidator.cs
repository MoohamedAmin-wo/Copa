using Cupa.MidatR.Common.RequestDTO;

namespace Cupa.MidatR.Users.Commands.Validators;

public class UpdatePhoneNumberValidator : AbstractValidator<PhoneNumberModelDTO>
{
    public UpdatePhoneNumberValidator()
    {
        RuleFor(x => x.Phone).NotEmpty().NotNull().WithMessage(ErrorMessages.RequiredField)
          .Matches(RegexPatterns.MobileNumber).WithMessage(ErrorMessages.InValidPhonenumber);
    }
}