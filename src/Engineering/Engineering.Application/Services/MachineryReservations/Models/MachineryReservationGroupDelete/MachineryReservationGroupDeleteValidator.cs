
namespace Engineering.Application.Services.MachineryReservations.Models.MachineryReservationGroupDelete;

public class MachineryReservationGroupDeleteValidator : AbstractValidator<MachineryReservationGroupDeleteRequest>
{
    public MachineryReservationGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
