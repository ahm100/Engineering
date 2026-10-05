namespace Engineering.Application.Services.Transportations.Commands.Disable;

public class DisableTransportationCommandValidator : AbstractValidator<DisableTransportationCommand>
{
    public DisableTransportationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationErrors.IdIsEmpty);
    }
}