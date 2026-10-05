namespace Engineering.Application.Services.Categories.Queries.GetsCategories;

public class GetsCategoriesQueryValidator : AbstractValidator<GetsCategoriesQuery>
{
    public GetsCategoriesQueryValidator()
    {
        RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);

        RuleFor(v => v.PageSize)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);

        When(v => v.PageSize > 0, () =>
        {
            RuleFor(v => v.PageIndex)
                .GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}