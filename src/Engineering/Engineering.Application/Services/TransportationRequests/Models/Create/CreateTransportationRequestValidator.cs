
namespace Engineering.Application.Services.TransportationRequests.Models.Create;

public class CreateTransportationRequestValidator : AbstractValidator<CreateTransportationRequestRequest>
{
    public CreateTransportationRequestValidator()
    {
        RuleFor(oo => oo.TransportationId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.TransportationId);
        RuleFor(oo => oo.TripId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.TripIdIsEmpty);
        RuleFor(oo => oo.MachineTypeId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.MachineIdIsEmpty);
        RuleFor(oo => oo.CostCenterIds).NotEmpty().WithError(TransportationRequestErrors.CostCenterIsEmpty);
        RuleFor(oo => oo.StartingCityId).NotNull().WithError(TransportationRequestErrors.StartingCityIdIsEmpty);
        RuleFor(oo => oo.DestinationCityId).NotNull().WithError(TransportationRequestErrors.DestinationCityIdIsEmpty);
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(TransportationRequestErrors.StartDateIsEmpty);
    }
}
