
namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetsRequestGoodSupplyRequester;

public record GetsRequestGoodSupplyRequesterQuery(
    ) : IQuery<DataResult<List<long>>>;
