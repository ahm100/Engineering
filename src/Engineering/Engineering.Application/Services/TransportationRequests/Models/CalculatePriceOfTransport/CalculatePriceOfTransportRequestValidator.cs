namespace Engineering.Application.Services.TransportationRequests.Models.CalculatePriceOfTransport;

public class CalculatePriceOfTransportRequestValidator : AbstractValidator<CalculatePriceOfTransportRequest>
{
    public CalculatePriceOfTransportRequestValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.IdIsEmpty);
        RuleFor(oo => oo.LoadWeight).NotNull().GreaterThanOrEqualTo(0).NotEmpty().WithError(TransportationRequestErrors.LoadWeightIsEmpty);
    }
}