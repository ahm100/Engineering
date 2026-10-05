namespace Engineering.Application.Services.Categories.Models.InactiveCategory;

public class InactiveCategoryValidator : AbstractValidator<InactiveCategoryRequest>
{
    public InactiveCategoryValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(GlobalCmts.CategoryId);
    }
}