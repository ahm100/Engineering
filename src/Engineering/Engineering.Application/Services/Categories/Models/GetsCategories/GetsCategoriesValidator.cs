namespace Engineering.Application.Services.Categories.Models.GetsCategories;

public class GetsCategoriesValidator : AbstractValidator<GetsCategoriesRequest>
{
    public GetsCategoriesValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}