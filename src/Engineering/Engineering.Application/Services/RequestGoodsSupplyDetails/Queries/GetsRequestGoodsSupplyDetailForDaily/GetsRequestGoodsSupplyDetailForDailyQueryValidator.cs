
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyDetailForDaily;

public class GetsRequestGoodsSupplyDetailForDailyQueryValidator : AbstractValidator<GetsRequestGoodsSupplyDetailForDailyQuery>
{
    public GetsRequestGoodsSupplyDetailForDailyQueryValidator()
    {
        RuleFor(c => c.ProjectOperationDetailId)
            .IsPositive(GlobalCmts.ProjectOperationDetailId);
    }
}
