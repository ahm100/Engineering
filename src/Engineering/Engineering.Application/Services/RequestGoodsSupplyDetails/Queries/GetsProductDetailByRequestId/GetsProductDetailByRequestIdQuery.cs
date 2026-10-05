using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsProductDetailByRequestId;

public record GetsProductDetailByRequestIdQuery(
       long RequestGoodsSupplyId
    ) : IQuery<DataResult<List<RequestGoodsSupplyDetail>>>;

