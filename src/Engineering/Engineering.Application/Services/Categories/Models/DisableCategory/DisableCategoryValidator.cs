namespace Engineering.Application.Services.Categories.Models.DisableCategory;

public class DisableCategoryValidator : AbstractValidator<DisableCategoryRequest>
{
    public DisableCategoryValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(GlobalCmts.CategoryId);

    }
}