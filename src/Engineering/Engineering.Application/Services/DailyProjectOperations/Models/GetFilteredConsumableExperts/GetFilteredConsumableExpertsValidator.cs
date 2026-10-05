namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableExperts;

public class GetFilteredConsumableExpertsValidator : AbstractValidator<GetFilteredConsumableExpertsRequest>
{
    public GetFilteredConsumableExpertsValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).GreaterThan(0).WithError(DailyProjectOperationErrors.InValidProjectOperationDetailId).NotNull().WithError(DailyProjectOperationErrors.InValidProjectOperationDetailId);
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
