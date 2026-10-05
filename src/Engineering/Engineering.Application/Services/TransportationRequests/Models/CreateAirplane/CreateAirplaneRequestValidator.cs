
namespace Engineering.Application.Services.TransportationRequests.Models.CreateAirplane;

public class CreateAirplaneRequestValidator : AbstractValidator<CreateAirplaneRequest>
{
    public CreateAirplaneRequestValidator()
    {
        RuleFor(oo => oo.TripId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.TripIdIsEmpty);
        RuleFor(oo => oo.StartingCityId).NotNull().WithError(TransportationRequestErrors.StartingCityIdIsEmpty);
        RuleFor(oo => oo.DestinationCityId).NotNull().WithError(TransportationRequestErrors.DestinationCityIdIsEmpty);
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(TransportationRequestErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotEmpty().WithError(TransportationRequestErrors.EndDateIsEmpty);
        RuleFor(oo => oo.CostCenterIds).NotEmpty().NotEmpty().WithError(TransportationRequestErrors.CostCenterIsEmpty);
    }
}
