
namespace Engineering.Application.Services.CostCenterVirtualGroups.Queries.CostCenterVirtualGroupById;

public class GetCostCenterVirtualGroupByCostCenterIdQueryValidator : AbstractValidator<GetCostCenterVirtualGroupByCostCenterIdQuery>
{
    public GetCostCenterVirtualGroupByCostCenterIdQueryValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(CostCenterVirtualGroupErrors.InValidCostCenter);
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
