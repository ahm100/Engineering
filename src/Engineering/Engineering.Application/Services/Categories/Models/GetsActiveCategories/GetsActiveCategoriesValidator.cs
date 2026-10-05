namespace Engineering.Application.Services.Categories.Models.GetsActiveCategories;

public class GetsActiveCategoriesValidator : AbstractValidator<GetsActiveCategoriesRequest>
{
    public GetsActiveCategoriesValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}