namespace Engineering.Application.Services.Categories.Models.UpdateCategory;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(GlobalCmts.CategoryId);

        RuleFor(v => v.CategoryName)
            .IsFullString(CategoryCmts.CategoryName, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(v => v.CategoryCode)
            .IsFullString(CategoryCmts.CategoryCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

    }
}