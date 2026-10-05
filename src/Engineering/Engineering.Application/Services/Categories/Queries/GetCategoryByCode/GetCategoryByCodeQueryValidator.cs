namespace Engineering.Application.Services.Categories.Queries.GetCategoryByCode;

public class GetCategoryByCodeQueryValidator : AbstractValidator<GetCategoryByCodeQuery>
{
    public GetCategoryByCodeQueryValidator()
    {
        RuleFor(v => v.CategoryCode)
            .NotEmpty().WithError(CategoryErrors.CategoryCodeIsEmpty);
    }
}