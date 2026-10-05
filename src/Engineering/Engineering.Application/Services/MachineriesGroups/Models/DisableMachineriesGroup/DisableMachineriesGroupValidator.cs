namespace Engineering.Application.Services.MachineriesGroups.Models.DisableMachineriesGroup;

public class DisableMachineriesGroupValidator : AbstractValidator<DisableMachineriesGroupRequest>
{
    public DisableMachineriesGroupValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.MachineriesGroupId);

    }
}
