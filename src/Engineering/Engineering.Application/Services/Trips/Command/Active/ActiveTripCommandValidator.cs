namespace Engineering.Application.Services.Trips.Commands.Active;

public class ActiveTripCommandValidator : AbstractValidator<ActiveTripCommand>
{
    public ActiveTripCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TripErrors.IdIsEmpty);
    }
}
