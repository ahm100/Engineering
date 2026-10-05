using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailBySupplyProductId;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailBySupplyProductId;

public record GetsGoodsSupplyDetailBySupplyProductIdQuery(
    long Id
    ) : IQuery<DataResult<List<GetsGoodsSupplyDetailBySupplyProductIdModel>>>;
