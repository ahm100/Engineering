namespace Engineering.Application.Services.Branchs.Models.ActiveBranch;

public class ActiveBranchValidator : AbstractValidator<ActiveBranchRequest>
{
    public ActiveBranchValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(GlobalCmts.BranchId);

    }
}