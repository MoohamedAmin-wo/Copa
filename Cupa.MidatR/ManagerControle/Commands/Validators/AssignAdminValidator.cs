using Cupa.MidatR.ManagerControle.Commands.DTOs;

namespace Cupa.MidatR.ManagerControle.Commands.Validators;

public sealed class AssignAdminValidator : AbstractValidator<AdminModelDTO>
{
    public AssignAdminValidator()
    {
        RuleFor(x => x.AdminEmail).NotEmpty().NotNull().WithMessage("Email can't be null or empty !")
           .Matches(RegexPatterns.EmailAddress).WithMessage(ErrorMessages.InValidEmail);
    }
}
