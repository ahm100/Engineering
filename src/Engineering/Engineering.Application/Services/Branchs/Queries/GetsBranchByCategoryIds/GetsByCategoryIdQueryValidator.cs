namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByCategoryIds;

public class GetsByCategoryIdQueryValidator : AbstractValidator<GetsBranchByCategoryIdsQuery>
{
    public GetsByCategoryIdQueryValidator()
    {
        RuleFor(v => v.CategoryIds)
            .NotEmpty().WithError(BranchErrors.CategoryIdIsEmpty)
            .NotNull().WithError(CategoryErrors.CategoryWithIdNotFound);

        RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);

        RuleFor(v => v.PageSize)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);

        When(w => w.PageSize > 0, () =>
        {
            RuleFor(v => v.PageIndex)
                .GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}