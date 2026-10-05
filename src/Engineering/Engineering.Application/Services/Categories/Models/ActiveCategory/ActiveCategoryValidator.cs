namespace Engineering.Application.Services.Categories.Models.ActiveCategory;

public class ActiveCategoryValidator : AbstractValidator<ActiveCategoryRequest>
{
    public ActiveCategoryValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(GlobalCmts.CategoryId);
    }
}