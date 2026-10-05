using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsGoodsSupplyManagmentBySupplyProductId;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetsGoodsSupplyManagmentBySupplyProductId;

public record GetsGoodsSupplyManagmentBySupplyProductIdQuery(
    long Id
    ) : IQuery<DataResult<List<GetsGoodsSupplyManagmentBySupplyProductIdModel>>>;