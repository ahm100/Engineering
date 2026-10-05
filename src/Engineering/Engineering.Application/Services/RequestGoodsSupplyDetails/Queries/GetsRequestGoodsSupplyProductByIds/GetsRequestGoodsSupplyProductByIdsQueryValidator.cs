namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyProductByIds;

public class GetsRequestGoodsSupplyProductByIdsQueryValidator : AbstractValidator<GetsRequestGoodsSupplyProductByIdsQuery>
{
    public GetsRequestGoodsSupplyProductByIdsQueryValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(GlobalCmts.Id);
    }
}
