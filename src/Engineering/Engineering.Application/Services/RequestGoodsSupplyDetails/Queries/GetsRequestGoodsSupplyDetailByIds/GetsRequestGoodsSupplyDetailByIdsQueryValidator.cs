namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyDetailByIds;

public class GetsRequestGoodsSupplyDetailByIdsQueryValidator : AbstractValidator<GetsRequestGoodsSupplyDetailByIdsQuery>
{
    public GetsRequestGoodsSupplyDetailByIdsQueryValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(GlobalCmts.Id);
    }
}
