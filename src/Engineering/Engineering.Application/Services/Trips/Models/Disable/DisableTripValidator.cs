
namespace Engineering.Application.Services.Trips.Models.Disable;

public class DisableTripValidator : AbstractValidator<DisableTripRequest>
{
    public DisableTripValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TripErrors.IdIsEmpty);
    }
}
