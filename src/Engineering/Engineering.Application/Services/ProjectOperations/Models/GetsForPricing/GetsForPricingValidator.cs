namespace Engineering.Application.Services.ProjectOperations.Models.GetsForPricing;

public class GetsForPricingValidator : AbstractValidator<GetsForPricingRequest>
{
    public GetsForPricingValidator()
    {
        RuleFor(oo => oo.EmployerId).NotNull().WithError(ProjectOperationErrors.EmployerIdIsEmpty);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.ProjectIdIsEmpty);
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(ProjectOperationErrors.CostCenterIdIsEmpty);
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
