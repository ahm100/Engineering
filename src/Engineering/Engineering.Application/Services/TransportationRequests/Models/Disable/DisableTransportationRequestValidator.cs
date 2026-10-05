
namespace Engineering.Application.Services.TransportationRequests.Models.Disable;

public class DisableTransportationRequestValidator : AbstractValidator<DisableTransportationRequestRequest>
{
    public DisableTransportationRequestValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
