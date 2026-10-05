using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyByIdForSeason;

public record GetRequestGoodsSupplyByIdForSeasonQuery(
    long Id
    ) : IQuery<RequestGoodsSupply>;

