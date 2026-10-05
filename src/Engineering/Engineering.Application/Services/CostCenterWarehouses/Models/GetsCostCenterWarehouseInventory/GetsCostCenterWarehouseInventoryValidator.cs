
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseInventory;

public class GetsCostCenterWarehouseInventoryValidator : AbstractValidator<GetsCostCenterWarehouseInventoryRequest>
{
    public GetsCostCenterWarehouseInventoryValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
        RuleFor(oo => oo.ProductId)
            .IsPositive(GlobalCmts.ProductId);
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
