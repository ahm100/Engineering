namespace Engineering.Application.RequestGoodsSupplyManagements.Commands.UpdateRequestGoodsSupplyManagement;

public class UpdateRequestGoodsSupplyManagementCommandValidator : AbstractValidator<UpdateRequestGoodsSupplyManagementCommand>
{
    public UpdateRequestGoodsSupplyManagementCommandValidator()
    {
        RuleFor(oo => oo.RequestedCount).GreaterThan(0).WithError(RequestGoodsSupplyManagementErrors.InValidWarehouseId);
        RuleFor(oo => oo.RequestGoodsSupplyManagementId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyManagementId);
        RuleFor(oo => oo.Type).IsInEnum().WithError(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyType);
        RuleFor(oo => oo.InvoiceId).NotNull().WithError(RequestGoodsSupplyManagementErrors.InValidInvoiceId);
    }
}
