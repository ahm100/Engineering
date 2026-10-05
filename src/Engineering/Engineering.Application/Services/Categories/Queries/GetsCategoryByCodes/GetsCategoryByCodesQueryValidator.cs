namespace Engineering.Application.Services.Categories.Queries.GetsCategoryByCodes;

public class GetsCategoryByCodesQueryValidator : AbstractValidator<GetsCategoryByCodesQuery>
{
    public GetsCategoryByCodesQueryValidator()
    {
        RuleFor(v => v.Codes)
            .NotEmpty().WithError(CategoryErrors.CategoryCodeIsEmpty);
    }
}