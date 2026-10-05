namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyProduct;

public class GetsRequestGoodsSupplyProductQueryValidator : AbstractValidator<GetsRequestGoodsSupplyProductQuery>
{
    public GetsRequestGoodsSupplyProductQueryValidator()
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
