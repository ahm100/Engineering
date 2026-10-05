namespace Engineering.Application.Services.Categories.Queries.GetsCategoryByIds;

public class GetsCategoryByIdsQueryValidator : AbstractValidator<GetsCategoryByIdsQuery>
{
    public GetsCategoryByIdsQueryValidator()
    {
        RuleFor(v => v.Items)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(CategoryErrors.CategoryWithIdNotFound);
    }
}