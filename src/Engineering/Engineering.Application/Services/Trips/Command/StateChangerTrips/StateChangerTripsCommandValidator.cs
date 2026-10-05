namespace Engineering.Application.Services.Trips.Commands.StateChangerTrips;

public class StateChangerTripsCommandValidator : AbstractValidator<StateChangerTripsCommand>
{
    public StateChangerTripsCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
