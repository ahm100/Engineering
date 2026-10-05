
namespace Engineering.Application.Services.MachineriesGroups.Models.StateChangerMachineriesGroups;

public class StateChangerMachineriesGroupsValidator : AbstractValidator<StateChangerMachineriesGroupsRequest>
{
    public StateChangerMachineriesGroupsValidator()
    {
        RuleForEach(oo => oo.Ids)
            .IsPositive(GlobalCmts.MachineriesGroupId);
    }
}
