namespace Engineering.Application.Services.Branchs.Models.GetsBranchByCategoryIds;

public class GetsBranchByCategoryIdsValidator : AbstractValidator<GetsBranchByCategoryIdsRequest>
{
    public GetsBranchByCategoryIdsValidator()
    {
        RuleForEach(v => v.CategoryIds)
            .IsPositive(GlobalCmts.CategoryId);

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