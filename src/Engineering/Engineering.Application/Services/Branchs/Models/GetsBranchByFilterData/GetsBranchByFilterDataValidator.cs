namespace Engineering.Application.Services.Branchs.Models.GetsBranchByFilterData;

public class GetsBranchByFilterDataValidator : AbstractValidator<GetsBranchByFilterDataRequest>
{
    public GetsBranchByFilterDataValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}