namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

public class GoodsSupplyManagementModelCommerceValidator : AbstractValidator<GoodsSupplyManagementModelCommerce>
{
    public GoodsSupplyManagementModelCommerceValidator()
    {
        RuleFor(oo => oo.DestinationWarehouseId).NotNull().WithError(RequestGoodsSupplyManagementErrors.DestinationWarehouseIdIsNull);
        RuleFor(oo => oo.RequestedCount).NotNull().WithError(RequestGoodsSupplyManagementErrors.InValidRequestedCount);
    }
}