
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedGoodsSupplyProduct;

public class SetConfirmedGoodsSupplyProductValidator : AbstractValidator<SetConfirmedGoodsSupplyProductModelRequest>
{
    public SetConfirmedGoodsSupplyProductValidator()
    {
        RuleFor(c => c.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId);
    }
}
