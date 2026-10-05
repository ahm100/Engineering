namespace Engineering.Application.Services.Branchs.Models.BranchGroupDelete;

public class BranchGroupDeleteValidator : AbstractValidator<BranchGroupDeleteRequest>
{
    public BranchGroupDeleteValidator()
    {
        RuleForEach(v => v.Ids)
            .IsPositive(GlobalCmts.BranchId);
    }
}