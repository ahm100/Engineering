namespace Engineering.Application.Services.Categories.Models.CategoryGroupDelete;

public class CategoryGroupDeleteValidator : AbstractValidator<CategoryGroupDeleteRequest>
{
    public CategoryGroupDeleteValidator()
    {
        RuleForEach(v => v.Ids)
            .IsPositive(GlobalCmts.CategoryId);
    }
}