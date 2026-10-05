using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyProductByIds;

public record GetsRequestGoodsSupplyProductByIdsQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<RequestGoodsSupplyProduct>>>;
