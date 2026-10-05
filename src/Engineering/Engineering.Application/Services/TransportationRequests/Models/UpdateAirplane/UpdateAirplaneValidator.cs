
namespace Engineering.Application.Services.TransportationRequests.Models.UpdateAirplane;

public class UpdateAirplaneValidator : AbstractValidator<UpdateAirplaneRequest>
{
    public UpdateAirplaneValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.IdIsEmpty);
        RuleFor(oo => oo.TripId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.TripIdIsEmpty);
        RuleFor(oo => oo.StartingCityId).NotNull().WithError(TransportationRequestErrors.StartingCityIdIsEmpty);
        RuleFor(oo => oo.DestinationCityId).NotNull().WithError(TransportationRequestErrors.DestinationCityIdIsEmpty);
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(TransportationRequestErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotEmpty().WithError(TransportationRequestErrors.EndDateIsEmpty);
        RuleFor(oo => oo.CostCenterIds).NotEmpty().WithError(TransportationRequestErrors.CostCenterIsEmpty);
    }
}
