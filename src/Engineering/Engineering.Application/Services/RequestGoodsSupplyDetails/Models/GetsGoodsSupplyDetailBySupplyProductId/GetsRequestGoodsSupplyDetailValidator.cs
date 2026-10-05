
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailBySupplyProductId;

public class GetsGoodsSupplyDetailBySupplyProductIdValidator : AbstractValidator<GetsGoodsSupplyDetailBySupplyProductIdRequest>
{
    public GetsGoodsSupplyDetailBySupplyProductIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}