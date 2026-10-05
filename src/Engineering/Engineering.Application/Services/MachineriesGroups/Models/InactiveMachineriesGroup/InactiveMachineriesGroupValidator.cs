namespace Engineering.Application.Services.MachineriesGroups.Models.InactiveMachineriesGroup;

public class InactiveMachineriesGroupValidator : AbstractValidator<InactiveMachineriesGroupRequest>
{
    public InactiveMachineriesGroupValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.MachineriesGroupId);
    }
}
