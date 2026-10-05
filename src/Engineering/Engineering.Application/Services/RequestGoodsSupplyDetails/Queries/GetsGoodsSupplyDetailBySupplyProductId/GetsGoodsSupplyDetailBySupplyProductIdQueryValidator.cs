namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailBySupplyProductId;

public class GetsGoodsSupplyDetailBySupplyProductIdQueryValidator : AbstractValidator<GetsGoodsSupplyDetailBySupplyProductIdQuery>
{
    public GetsGoodsSupplyDetailBySupplyProductIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
