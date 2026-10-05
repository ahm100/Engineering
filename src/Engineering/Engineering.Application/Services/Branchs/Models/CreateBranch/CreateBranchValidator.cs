namespace Engineering.Application.Services.Branchs.Models.CreateBranch;

public class CreateBranchValidator : AbstractValidator<CreateBranchRequest>
{
    public CreateBranchValidator()
    {
        RuleFor(v => v.CategoryId)
            .IsPositive(GlobalCmts.CategoryId);

        RuleFor(v => v.BranchName)
            .IsFullString(BranchCmts.BranchName, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(v => v.BranchCode)
            .IsFullString(BranchCmts.BranchName, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

    }
}