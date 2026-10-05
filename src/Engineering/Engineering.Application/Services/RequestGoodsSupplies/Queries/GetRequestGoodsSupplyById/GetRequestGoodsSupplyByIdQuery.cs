using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyById;

public record GetRequestGoodsSupplyByIdQuery(
    long Id
    ) : IQuery<RequestGoodsSupply>;

