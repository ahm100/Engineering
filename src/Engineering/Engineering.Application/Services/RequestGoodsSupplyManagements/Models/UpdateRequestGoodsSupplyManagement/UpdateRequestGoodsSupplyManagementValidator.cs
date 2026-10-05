namespace Engineering.Application.RequestGoodsSupplyManagements.Models.UpdateRequestGoodsSupplyManagement;

public class UpdateRequestGoodsSupplyManagementValidator : AbstractValidator<UpdateRequestGoodsSupplyManagementRequest>
{
    public UpdateRequestGoodsSupplyManagementValidator()
    {
        RuleFor(oo => oo.RequestedCount).GreaterThan(0).WithError(RequestGoodsSupplyManagementErrors.InValidWarehouseId);
        RuleFor(oo => oo.RequestGoodsSupplyManagementId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyManagementId);
        RuleFor(oo => oo.Type).IsInEnum().WithError(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyType);
    }
}
