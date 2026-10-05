namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedContractorGoodsSupply;

public class SetConfirmedContractorGoodsSupplyDetailValidator : AbstractValidator<SetConfirmedContractorGoodsSupplyDetail>
{
    public SetConfirmedContractorGoodsSupplyDetailValidator()
    {
        RuleFor(oo => oo.RequestGoodsSupplyDetailId).NotNull().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyDetail);
        RuleFor(oo => oo.DestinationWarehouseId).NotEmpty().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyManagementErrors.DestinationWarehouseIdIsNull);
    }
}
