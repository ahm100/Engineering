using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyDetailByIds;

public record GetsRequestGoodsSupplyDetailByIdsQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<RequestGoodsSupplyDetail>>>;
