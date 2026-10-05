namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetFilteredDailyProjectOperationDetails;

public class GetFilteredDailyProjectOperationDetailsQueryValidator : AbstractValidator<GetFilteredDailyProjectOperationDetailsQuery>
{
    public GetFilteredDailyProjectOperationDetailsQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().WithError(DailyProjectOperationErrors.InValidProjectOperationId);
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
