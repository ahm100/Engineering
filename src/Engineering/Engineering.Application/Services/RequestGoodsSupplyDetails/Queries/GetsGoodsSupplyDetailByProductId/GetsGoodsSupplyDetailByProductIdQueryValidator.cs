namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailByProductId;

public class GetsGoodsSupplyDetailByProductIdQueryValidator : AbstractValidator<GetsGoodsSupplyDetailByProductIdQuery>
{
    public GetsGoodsSupplyDetailByProductIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
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
