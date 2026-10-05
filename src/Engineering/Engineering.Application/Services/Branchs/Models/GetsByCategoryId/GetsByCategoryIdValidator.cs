namespace Engineering.Application.Services.Branchs.Models.GetsByCategoryId;

public class GetsByCategoryIdValidator : AbstractValidator<GetsByCategoryIdRequest>
{
    public GetsByCategoryIdValidator()
    {
        RuleFor(v => v.CategoryId)
            .IsPositive(GlobalCmts.BranchId);

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