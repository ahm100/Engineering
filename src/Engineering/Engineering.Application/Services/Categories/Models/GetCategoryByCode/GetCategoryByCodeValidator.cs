namespace Engineering.Application.Services.Categories.Models.GetCategoryByCode;

public class GetCategoryByCodeValidator : AbstractValidator<GetCategoryByCodeRequest>
{
    public GetCategoryByCodeValidator()
    {
        RuleFor(v => v.CategoryCode)
            .IsRequiredString(CategoryCmts.CategoryCode);
    }
}