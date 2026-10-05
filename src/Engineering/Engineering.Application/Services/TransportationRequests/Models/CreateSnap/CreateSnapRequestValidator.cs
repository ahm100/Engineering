
namespace Engineering.Application.Services.TransportationRequests.Models.CreateSnap;

public class CreateSnapRequestValidator : AbstractValidator<CreateSnapRequest>
{
    public CreateSnapRequestValidator()
    {
        RuleFor(oo => oo.TripId).NotNull().WithError(TransportationRequestErrors.TripIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.StartingCityAddress).NotNull().WithError(TransportationRequestErrors.StartingCityAddressIsEmpty);
        RuleFor(oo => oo.DestinationAddress).NotNull().WithError(TransportationRequestErrors.DestinationCityAddressIsEmpty);
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(TransportationRequestErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotEmpty().WithError(TransportationRequestErrors.EndDateIsEmpty);
        RuleFor(oo => oo.CostCenterIds).NotEmpty().WithError(TransportationRequestErrors.CostCenterIsEmpty);
    }
}
