namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailBySupplyProductIdWithScale;

public class GetsGoodsSupplyDetailBySupplyProductIdWithScaleQueryValidator : AbstractValidator<GetsGoodsSupplyDetailBySupplyProductIdWithScaleQuery>
{
    public GetsGoodsSupplyDetailBySupplyProductIdWithScaleQueryValidator()
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
