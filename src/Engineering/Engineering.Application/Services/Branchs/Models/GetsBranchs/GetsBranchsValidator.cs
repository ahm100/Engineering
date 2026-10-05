namespace Engineering.Application.Services.Branchs.Models.GetsBranchs;

public class GetsBranchsValidator : AbstractValidator<GetsBranchsRequest>
{
    public GetsBranchsValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}