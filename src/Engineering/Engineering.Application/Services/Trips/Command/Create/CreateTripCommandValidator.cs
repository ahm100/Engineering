namespace Engineering.Application.Services.Trips.Commands.Create;

public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
{
    public CreateTripCommandValidator()
    {
        RuleFor(oo => oo.TripName).NotEmpty().WithError(TripErrors.NameIsEmpty);
        RuleFor(oo => oo.TripCode).NotEmpty().WithError(TripErrors.CodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(TripErrors.IsActiveIsEmpty);
    }
}
