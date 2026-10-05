namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFilteredFiduciaryProductManages;

public class GetFilteredFiduciaryProductManagesValidator : AbstractValidator<GetFilteredFiduciaryProductManagesRequest>
{
    public GetFilteredFiduciaryProductManagesValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(FiduciaryProductErrors.InValidCostCenter);
        RuleFor(oo => oo.ProjectId).NotNull().WithError(FiduciaryProductErrors.InValidProject);
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
