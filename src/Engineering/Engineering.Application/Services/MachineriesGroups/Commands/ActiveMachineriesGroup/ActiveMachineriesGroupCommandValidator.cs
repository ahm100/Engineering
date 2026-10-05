namespace Engineering.Application.Services.MachineriesGroups.Commands.ActiveMachineriesGroup;

public class ActiveMachineriesGroupCommandValidator : AbstractValidator<ActiveMachineriesGroupCommand>
{
    public ActiveMachineriesGroupCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineriesGroupErrors.IdIsEmpty);
    }
}