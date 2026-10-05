namespace Engineering.Application.Services.Categories.Models.GetCategoryByName;

public class GetCategoryByNameValidator : AbstractValidator<GetCategoryByNameRequest>
{
    public GetCategoryByNameValidator()
    {
        RuleFor(v => v.CategoryName)
            .IsRequiredString(CategoryCmts.CategoryName);
    }
}