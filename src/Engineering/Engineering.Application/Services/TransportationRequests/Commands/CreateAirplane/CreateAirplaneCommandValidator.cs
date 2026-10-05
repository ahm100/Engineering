namespace Engineering.Application.Services.TransportationRequests.Commands.CreateAirplane;

public class CreateAirplaneCommandValidator : AbstractValidator<CreateAirplaneCommand>
{
    public CreateAirplaneCommandValidator()
    {
        RuleFor(oo => oo.Transportation).NotNull().WithError(TransportationRequestErrors.TransportationId);
        RuleFor(oo => oo.Trip).NotNull().WithError(TransportationRequestErrors.TripIdIsEmpty);
        RuleFor(oo => oo.StartingCityId).NotNull().WithError(TransportationRequestErrors.StartingCityIdIsEmpty);
        RuleFor(oo => oo.DestinationCityId).NotNull().WithError(TransportationRequestErrors.DestinationCityIdIsEmpty);
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(TransportationRequestErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotEmpty().WithError(TransportationRequestErrors.EndDateIsEmpty);
        RuleFor(oo => oo.CostCenters).NotEmpty().WithError(TransportationRequestErrors.CostCenterIsEmpty);
    }
}
