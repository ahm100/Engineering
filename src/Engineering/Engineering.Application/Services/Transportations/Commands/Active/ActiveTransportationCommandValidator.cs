namespace Engineering.Application.Services.Transportations.Commands.Active;

public class ActiveTransportationCommandValidator : AbstractValidator<ActiveTransportationCommand>
{
    public ActiveTransportationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationErrors.IdIsEmpty);
    }
}
