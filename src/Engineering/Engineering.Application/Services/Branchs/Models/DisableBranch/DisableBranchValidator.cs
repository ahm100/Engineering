namespace Engineering.Application.Services.Branchs.Models.DisableBranch;

public class DisableBranchValidator : AbstractValidator<DisableBranchRequest>
{
    public DisableBranchValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(GlobalCmts.BranchId);

    }
}