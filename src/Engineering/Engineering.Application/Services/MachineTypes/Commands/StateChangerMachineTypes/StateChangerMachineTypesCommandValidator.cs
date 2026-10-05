namespace Engineering.Application.Services.MachineTypes.Commands.StateChangerMachineTypes;

public class StateChangerMachineTypesCommandValidator : AbstractValidator<StateChangerMachineTypesCommand>
{
    public StateChangerMachineTypesCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
