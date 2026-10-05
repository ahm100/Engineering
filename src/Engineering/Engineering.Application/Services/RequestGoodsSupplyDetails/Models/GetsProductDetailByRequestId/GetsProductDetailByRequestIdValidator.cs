
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProductDetailByRequestId;

public class GetsProductDetailByRequestIdValidator : AbstractValidator<GetsProductDetailByRequestIdRequest>
{
    public GetsProductDetailByRequestIdValidator()
    {
        RuleFor(c => c.RequestGoodsSupplyId)
            .IsPositive(GlobalCmts.RequestGoodsSupplyId);
    }
}
