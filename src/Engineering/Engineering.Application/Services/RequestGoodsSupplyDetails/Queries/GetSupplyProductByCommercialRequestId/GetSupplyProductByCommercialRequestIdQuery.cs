using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetSupplyProductByCommercialRequestId;

public record GetSupplyProductByCommercialRequestIdQuery(
    long CommercialRequestId
    ) : IQuery<RequestGoodsSupplyProduct>;

