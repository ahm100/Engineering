using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetRequestGoodsSupplyManagementById;

public record GetRequestGoodsSupplyManagementByIdQuery(
    long Id
    ) : IQuery<RequestGoodsSupplyManagement>;
