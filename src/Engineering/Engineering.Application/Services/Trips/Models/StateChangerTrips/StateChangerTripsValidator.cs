
namespace Engineering.Application.Services.Trips.Models.StateChangerTrips;

public class StateChangerTripsValidator : AbstractValidator<StateChangerTripsRequest>
{
    public StateChangerTripsValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
