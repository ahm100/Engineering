using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetRequestGoodsManagementHistoryById;

public record GetRequestGoodsManagementHistoryByIdQuery(
    long Id,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<RequestGoodsSupplyManagementHistory>>>;
