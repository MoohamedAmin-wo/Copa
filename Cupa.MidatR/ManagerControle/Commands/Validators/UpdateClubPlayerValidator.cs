using Cupa.MidatR.ManagerControle.Commands.DTOs;
namespace Cupa.MidatR.ManagerControle.Commands.Validators;
public class UpdateClubPlayerValidator : AbstractValidator<UpdateClubPlayerModelDTO>
{
    public UpdateClubPlayerValidator()
    {
        RuleFor(x => x.PositionId).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredField)
          .Matches(RegexPatterns.AllowedPosition).WithMessage("invalid position selected ");

        RuleFor(x => x.Number).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredField)
            .GreaterThanOrEqualTo(1).LessThanOrEqualTo(99);

        RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage(ErrorMessages.RequiredField)
            .GreaterThanOrEqualTo(1000);
    }
}