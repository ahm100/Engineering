
namespace Engineering.Application.Services.Trips.Commands.Inactive;

public class InactiveTripCommandValidator : AbstractValidator<InactiveTripCommand>
{
    public InactiveTripCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TripErrors.IdIsEmpty);
    }
}
