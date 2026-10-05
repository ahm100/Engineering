
namespace Engineering.Application.Services.TransportationRequests.Models.UpdateSnap;

public class UpdateSnapValidator : AbstractValidator<UpdateSnapRequest>
{
    public UpdateSnapValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(TransportationRequestErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.TripId).NotNull().WithError(TransportationRequestErrors.TripIdIsEmpty)
              .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.StartingCityAddress).NotNull().WithError(TransportationRequestErrors.StartingCityAddressIsEmpty);
        RuleFor(oo => oo.DestinationAddress).NotNull().WithError(TransportationRequestErrors.DestinationCityAddressIsEmpty);
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(TransportationRequestErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotEmpty().WithError(TransportationRequestErrors.EndDateIsEmpty);
        RuleFor(oo => oo.CostCenterIds).NotEmpty().WithError(TransportationRequestErrors.CostCenterIsEmpty);
    }
}
