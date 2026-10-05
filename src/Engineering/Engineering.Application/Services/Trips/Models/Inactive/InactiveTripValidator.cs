
namespace Engineering.Application.Services.Trips.Models.Inactive;

public class InactiveTripValidator : AbstractValidator<InactiveTripRequest>
{
    public InactiveTripValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TripErrors.IdIsEmpty);
    }
}
