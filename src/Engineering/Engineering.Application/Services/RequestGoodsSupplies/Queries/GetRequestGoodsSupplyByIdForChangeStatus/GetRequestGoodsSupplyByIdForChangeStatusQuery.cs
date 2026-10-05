using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyByIdForChangeStatus;

public record GetRequestGoodsSupplyByIdForChangeStatusQuery(
    long Id
    ) : IQuery<RequestGoodsSupply>;

