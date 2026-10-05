namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsGoodsSupplyManagmentBySupplyProductId;

public class GetsGoodsSupplyManagmentBySupplyProductIdValidator : AbstractValidator<GetsGoodsSupplyManagmentBySupplyProductIdRequest>
{
    public GetsGoodsSupplyManagmentBySupplyProductIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId);
    }
}
