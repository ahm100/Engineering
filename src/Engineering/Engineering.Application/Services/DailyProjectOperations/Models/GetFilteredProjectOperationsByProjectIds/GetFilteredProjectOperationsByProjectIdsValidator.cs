namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredProjectOperationsByProjectIds;

public class GetFilteredProjectOperationsByProjectIdsValidator : AbstractValidator<GetFilteredProjectOperationsByProjectIdsRequest>
{
    public GetFilteredProjectOperationsByProjectIdsValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(DailyProjectOperationErrors.CostCenterIdNotBeNull)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.ProjectIds).NotNull().NotEmpty().WithError(DailyProjectOperationErrors.ProjectIdNotBeNull);
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
