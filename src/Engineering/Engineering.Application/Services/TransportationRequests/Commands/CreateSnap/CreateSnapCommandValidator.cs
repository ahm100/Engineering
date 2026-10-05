namespace Engineering.Application.Services.TransportationRequests.Commands.CreateSnap;

public class CreateSnapCommandValidator : AbstractValidator<CreateSnapCommand>
{
    public CreateSnapCommandValidator()
    {
        RuleFor(oo => oo.Trip).NotNull().WithError(TransportationRequestErrors.TripIdIsEmpty);
        RuleFor(oo => oo.StartingCityAddress).NotNull().WithError(TransportationRequestErrors.StartingCityAddressIsEmpty);
        RuleFor(oo => oo.DestinationAddress).NotNull().WithError(TransportationRequestErrors.DestinationCityAddressIsEmpty);
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(TransportationRequestErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotEmpty().WithError(TransportationRequestErrors.EndDateIsEmpty);
        RuleFor(oo => oo.CostCenters).NotEmpty().WithError(TransportationRequestErrors.CostCenterIsEmpty);
    }
}
