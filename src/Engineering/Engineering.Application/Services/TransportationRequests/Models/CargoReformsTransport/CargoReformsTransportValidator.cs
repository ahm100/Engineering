namespace Engineering.Application.Services.TransportationRequests.Models.CargoReformsTransport;

public class CargoReformsTransportValidator : AbstractValidator<CargoReformsTransportRequest>
{
    public CargoReformsTransportValidator()
    {
        When(x => x.PalletIds.Count > 0, () =>
        {
            RuleForEach(oo => oo.PalletIds)
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .NotEmpty()
                .WithError(TransportationRequestErrors.UnvalidPacking);
        });

        RuleFor(oo => oo.Id)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .NotEmpty()
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}