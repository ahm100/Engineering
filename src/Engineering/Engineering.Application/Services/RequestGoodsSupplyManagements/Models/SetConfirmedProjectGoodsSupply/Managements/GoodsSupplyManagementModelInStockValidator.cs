namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

public class GoodsSupplyManagementModelInStockValidator : AbstractValidator<GoodsSupplyManagementModelInStock>
{
    public GoodsSupplyManagementModelInStockValidator()
    {
        RuleFor(oo => oo.WarehouseId).NotNull().WithError(RequestGoodsSupplyManagementErrors.InValidWarehouseId);
        RuleFor(oo => oo.RequestedCount).NotNull().WithError(RequestGoodsSupplyManagementErrors.InValidRequestedCount);
    }
}