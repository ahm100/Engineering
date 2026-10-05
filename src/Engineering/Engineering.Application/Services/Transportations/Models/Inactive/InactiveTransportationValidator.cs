
namespace Engineering.Application.Services.Transportations.Models.Inactive;

public class InactiveTransportationValidator : AbstractValidator<InactiveTransportationRequest>
{
    public InactiveTransportationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationErrors.IdIsEmpty);
    }
}
