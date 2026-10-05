namespace Engineering.Application.Services.Categories.Queries.IsDuplicateCategoryByCode;

public class IsDuplicateCategoryByCodeQueryValidator : AbstractValidator<IsDuplicateCategoryByCodeQuery>
{
    public IsDuplicateCategoryByCodeQueryValidator()
    {
        RuleFor(v => v.CategoryCode)
            .NotEmpty().WithError(CategoryErrors.CategoryCodeIsEmpty);
    }
}