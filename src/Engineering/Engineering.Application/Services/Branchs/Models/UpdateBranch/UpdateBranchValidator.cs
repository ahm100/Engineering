namespace Engineering.Application.Services.Branchs.Models.UpdateBranch;

public class UpdateBranchValidator : AbstractValidator<UpdateBranchRequest>
{
    public UpdateBranchValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(GlobalCmts.BranchId);

        RuleFor(v => v.CategoryId)
            .IsPositive(GlobalCmts.CategoryId);

        RuleFor(v => v.BranchName)
            .IsFullString(BranchCmts.BranchName, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(v => v.BranchCode)
            .IsFullString(BranchCmts.BranchCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}