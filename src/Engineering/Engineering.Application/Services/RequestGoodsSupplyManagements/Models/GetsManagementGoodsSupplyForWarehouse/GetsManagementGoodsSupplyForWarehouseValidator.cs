namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetsManagementGoodsSupplyForWarehouse;

public class GetsManagementGoodsSupplyForWarehouseValidator : AbstractValidator<GetsManagementGoodsSupplyForWarehouseRequest>
{
    public GetsManagementGoodsSupplyForWarehouseValidator()
    {
        RuleFor(c => c.ProductId).NotNull().WithError(RequestGoodsSupplyManagementErrors.ProductIdIsEmpty);
    }
}
