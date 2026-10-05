
namespace Engineering.Application.Services.TransportationRequests.Commands.CalculatePriceOfTransport;

public class CalculatePriceOfTransportCommandValidator : AbstractValidator<CalculatePriceOfTransportCommand>
{
    public CalculatePriceOfTransportCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.RequestNotValid);
        RuleFor(oo => oo.LoadWeight).NotNull().GreaterThan(0).WithError(TransportationRequestErrors.LoadWeightIsEmpty);
    }
}
