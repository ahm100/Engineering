
namespace Engineering.Application.Services.Trips.Models.Active;

public class ActiveTripValidator : AbstractValidator<ActiveTripRequest>
{
    public ActiveTripValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TripErrors.IdIsEmpty);
    }
}
