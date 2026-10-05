using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsDetailHistoryById;

public record GetRequestGoodsDetailHistoryByIdQuery(
    long Id,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<RequestGoodsSupplyDetailHistory>>>;
