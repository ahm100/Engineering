
namespace Engineering.Application.Services.Transportations.Commands.Inactive;

public class InactiveTransportationCommandValidator : AbstractValidator<InactiveTransportationCommand>
{
    public InactiveTransportationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationErrors.IdIsEmpty);
    }
}