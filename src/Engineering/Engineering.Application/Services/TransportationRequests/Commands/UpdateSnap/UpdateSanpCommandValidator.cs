
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateSanp;

public class UpdateSanpCommandValidator : AbstractValidator<UpdateSanpCommand>
{
    public UpdateSanpCommandValidator()
    {
        RuleFor(oo => oo.Transportation).NotNull().WithError(TransportationRequestErrors.TransportationId);
        RuleFor(oo => oo.Trip).NotNull().WithError(TransportationRequestErrors.TripIdIsEmpty);
        RuleFor(oo => oo.StartingCityAddress).NotNull().WithError(TransportationRequestErrors.StartingCityAddressIsEmpty);
        RuleFor(oo => oo.DestinationAddress).NotNull().WithError(TransportationRequestErrors.DestinationCityAddressIsEmpty);
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(TransportationRequestErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotEmpty().WithError(TransportationRequestErrors.EndDateIsEmpty);
        RuleFor(oo => oo.CostCenters).NotEmpty().WithError(TransportationRequestErrors.CostCenterIsEmpty);
    }
}
