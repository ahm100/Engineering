namespace Engineering.Application.Services.Transportations.Commands.StateChangerTransportations;

public class StateChangerTransportationsCommandValidator : AbstractValidator<StateChangerTransportationsCommand>
{
    public StateChangerTransportationsCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
