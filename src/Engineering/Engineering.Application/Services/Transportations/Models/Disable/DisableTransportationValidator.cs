
namespace Engineering.Application.Services.Transportations.Models.Disable;

public class DisableTransportationValidator : AbstractValidator<DisableTransportationRequest>
{
    public DisableTransportationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationErrors.IdIsEmpty);
    }
}
