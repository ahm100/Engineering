
namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetsCostCenterWarehouseInventory;

public class GetsCostCenterWarehouseInventoryQueryValidator : AbstractValidator<GetsCostCenterWarehouseInventoryQuery>
{
    public GetsCostCenterWarehouseInventoryQueryValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(CostCenterWarehouseErrors.CostCenterIdIsEmpty);
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
