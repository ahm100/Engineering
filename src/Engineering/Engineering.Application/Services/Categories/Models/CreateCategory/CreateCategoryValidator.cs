namespace Engineering.Application.Services.Categories.Models.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(v => v.CategoryCode)
            .IsFullString(CategoryCmts.CategoryCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);


        RuleFor(v => v.CategoryName)
            .IsFullString(CategoryCmts.CategoryName, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

    }
}