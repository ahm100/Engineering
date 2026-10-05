namespace Engineering.Application.Services.Seasons.Queries.GetsByBranchIdForResponse;

public class GetsByBranchIdForResponseQueryValidator : AbstractValidator<GetsByBranchIdForResponseQuery>
{
    public GetsByBranchIdForResponseQueryValidator()
    {
        RuleFor(oo => oo.BranchId).NotNull().GreaterThanOrEqualTo(1).WithError(SeasonErrors.BranchIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}