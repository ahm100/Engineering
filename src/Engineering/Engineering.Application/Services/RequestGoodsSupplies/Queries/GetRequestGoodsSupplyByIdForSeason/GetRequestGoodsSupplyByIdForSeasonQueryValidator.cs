
namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyByIdForSeason;

public class GetRequestGoodsSupplyByIdForSeasonQueryValidator : AbstractValidator<GetRequestGoodsSupplyByIdForSeasonQuery>
{
    public GetRequestGoodsSupplyByIdForSeasonQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
