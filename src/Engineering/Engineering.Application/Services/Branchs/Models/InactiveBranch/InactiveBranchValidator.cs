namespace Engineering.Application.Services.Branchs.Models.InactiveBranch;

public class InactiveBranchValidator : AbstractValidator<InactiveBranchRequest>
{
    public InactiveBranchValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(GlobalCmts.BranchId);

    }
}