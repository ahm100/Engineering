namespace Engineering.Application.Services.Trips.Commands.Disable;

public class DisableTripCommandValidator : AbstractValidator<DisableTripCommand>
{
    public DisableTripCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TripErrors.IdIsEmpty);
    }
}
