using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyDetail;

public record GetsRequestGoodsSupplyDetailQuery(
    long Id,
    List<GoodsSupplyDetailStatus>? Statuses,
    List<GoodsSupplyDetailStatus>? RemoveStatuses,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<RequestGoodsSupplyDetail>>>;
