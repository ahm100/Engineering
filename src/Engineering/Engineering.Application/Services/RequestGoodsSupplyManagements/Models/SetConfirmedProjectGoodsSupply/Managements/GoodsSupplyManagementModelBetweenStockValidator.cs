namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

public class GoodsSupplyManagementModelBetweenStockValidator : AbstractValidator<GoodsSupplyManagementModelBetweenStock>
{
    public GoodsSupplyManagementModelBetweenStockValidator()
    {
        RuleFor(oo => oo.WarehouseId).NotNull().WithError(RequestGoodsSupplyManagementErrors.InValidWarehouseId);
        RuleFor(oo => oo.DestinationWarehouseId).NotNull().WithError(RequestGoodsSupplyManagementErrors.DestinationWarehouseIdIsNull);
        RuleFor(oo => oo.RequestedCount).NotNull().WithError(RequestGoodsSupplyManagementErrors.InValidRequestedCount);
    }
}