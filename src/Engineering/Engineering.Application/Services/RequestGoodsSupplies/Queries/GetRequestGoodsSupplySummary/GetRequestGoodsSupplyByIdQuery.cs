using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplySummary;

public record GetRequestGoodsSupplySummaryQuery(
    long Id
    ) : IQuery<RequestGoodsSupply>;

