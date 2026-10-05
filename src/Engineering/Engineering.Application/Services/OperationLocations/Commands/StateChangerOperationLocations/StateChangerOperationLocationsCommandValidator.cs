namespace Engineering.Application.Services.OperationLocations.Commands.StateChangerOperationLocations;

public class StateChangerOperationLocationsCommandValidator : AbstractValidator<StateChangerOperationLocationsCommand>
{
    public StateChangerOperationLocationsCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
