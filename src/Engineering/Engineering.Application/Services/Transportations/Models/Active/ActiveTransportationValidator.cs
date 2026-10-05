
namespace Engineering.Application.Services.Transportations.Models.Active;

public class ActiveTransportationValidator : AbstractValidator<ActiveTransportationRequest>
{
    public ActiveTransportationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationErrors.IdIsEmpty);
    }
}
