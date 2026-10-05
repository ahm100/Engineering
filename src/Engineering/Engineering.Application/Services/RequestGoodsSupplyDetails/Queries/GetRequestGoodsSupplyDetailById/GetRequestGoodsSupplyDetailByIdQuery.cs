using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyDetailById;

public record GetRequestGoodsSupplyDetailByIdQuery(
    long Id
    ) : IQuery<RequestGoodsSupplyDetail>;

