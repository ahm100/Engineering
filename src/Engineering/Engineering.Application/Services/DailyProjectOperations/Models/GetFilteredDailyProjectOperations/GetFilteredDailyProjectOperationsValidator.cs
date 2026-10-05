namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredDailyProjectOperations;

public class GetFilteredDailyProjectOperationsValidator : AbstractValidator<GetFilteredDailyProjectOperationsRequest>
{
    public GetFilteredDailyProjectOperationsValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(DailyProjectOperationErrors.CostCenterIdNotBeNull);
        RuleFor(oo => oo.ProjectId).NotNull().WithError(DailyProjectOperationErrors.ProjectIdNotBeNull);
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
