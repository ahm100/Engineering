namespace Engineering.Application.RequestGoodsSupplyManagements.Models.RejectRequestGoodsSupplyManagement;

public class RejectRequestGoodsSupplyManagementValidator : AbstractValidator<RejectRequestGoodsSupplyManagementRequest>
{
    public RejectRequestGoodsSupplyManagementValidator()
    {
        RuleFor(c => c.InvoiceId).NotNull().WithError(RequestGoodsSupplyManagementErrors.InvoiceIdIsEmpty);
    }
}
