namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByCategoryId;

public class GetsBranchByCategoryIdQueryValidator : AbstractValidator<GetsBranchByCategoryIdQuery>
{
    public GetsBranchByCategoryIdQueryValidator()
    {
        RuleFor(v => v.CategoryId)
            .NotNull().WithError(CategoryErrors.CategoryWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

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