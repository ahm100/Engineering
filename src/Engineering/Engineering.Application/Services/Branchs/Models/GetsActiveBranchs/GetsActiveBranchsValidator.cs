namespace Engineering.Application.Services.Branchs.Models.GetsActiveBranchs;

public class GetsActiveBranchsValidator : AbstractValidator<GetsActiveBranchsRequest>
{
    public GetsActiveBranchsValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}