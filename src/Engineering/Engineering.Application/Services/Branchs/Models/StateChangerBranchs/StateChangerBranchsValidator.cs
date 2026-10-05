namespace Engineering.Application.Services.Branchs.Models.StateChangerBranchs;

public class StateChangerBranchsValidator : AbstractValidator<StateChangerBranchsRequest>
{
    public StateChangerBranchsValidator()
    {
        RuleForEach(v => v.Ids)
            .IsPositive(GlobalCmts.BranchId);

    }
}