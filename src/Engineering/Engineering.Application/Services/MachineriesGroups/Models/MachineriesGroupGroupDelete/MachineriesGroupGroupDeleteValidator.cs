
namespace Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupGroupDelete;

public class MachineriesGroupGroupDeleteValidator : AbstractValidator<MachineriesGroupGroupDeleteRequest>
{
    public MachineriesGroupGroupDeleteValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(GlobalCmts.MachineriesGroupId);
    }
}
