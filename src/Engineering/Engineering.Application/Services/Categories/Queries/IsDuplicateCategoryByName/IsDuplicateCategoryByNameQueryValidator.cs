namespace Engineering.Application.Services.Categories.Queries.IsDuplicateCategoryByName;

public class IsDuplicateCategoryByNameQueryValidator : AbstractValidator<IsDuplicateCategoryByNameQuery>
{
    public IsDuplicateCategoryByNameQueryValidator()
    {
        RuleFor(v => v.CategoryName)
            .NotEmpty().WithError(CategoryErrors.CategoryNameIsEmpty);
    }
}