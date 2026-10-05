
namespace Engineering.Application.Services.TransportationRequests.Commands.Disable;

public class DisableTransportationRequestCommandValidator : AbstractValidator<DisableTransportationRequestCommand>
{
    public DisableTransportationRequestCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
