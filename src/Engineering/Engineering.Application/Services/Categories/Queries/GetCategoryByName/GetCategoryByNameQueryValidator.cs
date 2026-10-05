namespace Engineering.Application.Services.Categories.Queries.GetCategoryByName;

public class GetCategoryByNameQueryValidator : AbstractValidator<GetCategoryByNameQuery>
{
    public GetCategoryByNameQueryValidator()
    {
        RuleFor(v => v.CategoryName)
            .NotEmpty().WithError(CategoryErrors.CategoryNameIsEmpty);
    }
}