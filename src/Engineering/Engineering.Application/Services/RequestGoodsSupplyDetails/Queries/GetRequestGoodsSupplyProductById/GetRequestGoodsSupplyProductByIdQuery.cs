using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyProductById;

public record GetRequestGoodsSupplyProductByIdQuery(
    long Id
    ) : IQuery<RequestGoodsSupplyProduct>;

