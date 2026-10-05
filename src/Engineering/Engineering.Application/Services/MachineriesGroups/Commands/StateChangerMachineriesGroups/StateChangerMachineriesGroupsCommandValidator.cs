namespace Engineering.Application.Services.MachineriesGroups.Commands.StateChangerMachineriesGroups;

public class StateChangerMachineriesGroupsCommandValidator : AbstractValidator<StateChangerMachineriesGroupsCommand>
{
    public StateChangerMachineriesGroupsCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
