
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProduct;

public class GetsGoodsSupplyProductValidator : AbstractValidator<GetsGoodsSupplyProductRequest>
{
    public GetsGoodsSupplyProductValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
