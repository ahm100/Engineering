namespace Engineering.Application.Services.Machineries.Commands.StateChangerMachineries;

public class StateChangerMachineriesCommandValidator : AbstractValidator<StateChangerMachineriesCommand>
{
    public StateChangerMachineriesCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
