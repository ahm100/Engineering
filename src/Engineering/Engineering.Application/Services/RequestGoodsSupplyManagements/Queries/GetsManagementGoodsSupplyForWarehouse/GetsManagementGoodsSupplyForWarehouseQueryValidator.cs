namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetsManagementGoodsSupplyForWarehouse;

public class GetsManagementGoodsSupplyForWarehouseQueryValidator : AbstractValidator<GetsManagementGoodsSupplyForWarehouseQuery>
{
    public GetsManagementGoodsSupplyForWarehouseQueryValidator()
    {
        RuleFor(c => c.ProductId).NotNull().WithError(RequestGoodsSupplyManagementErrors.ProductIdIsEmpty);
    }
}
