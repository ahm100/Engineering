using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsHistoryById;

public record GetRequestGoodsHistoryByIdQuery(
    long Id,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<RequestGoodsSupplyHistory>>>;
