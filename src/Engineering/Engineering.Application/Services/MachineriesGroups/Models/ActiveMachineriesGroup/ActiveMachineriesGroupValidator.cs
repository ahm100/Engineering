namespace Engineering.Application.Services.MachineriesGroups.Models.ActiveMachineriesGroup;

public class ActiveMachineriesGroupValidator : AbstractValidator<ActiveMachineriesGroupRequest>
{
    public ActiveMachineriesGroupValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.MachineriesGroupId);
    }
}
