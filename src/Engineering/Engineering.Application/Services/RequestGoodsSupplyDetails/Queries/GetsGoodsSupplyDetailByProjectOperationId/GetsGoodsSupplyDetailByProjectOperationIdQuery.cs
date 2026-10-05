using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailByProjectOperationId;

public record GetsGoodsSupplyDetailByProjectOperationIdQuery(
    long ProjectOperationId
    ) : IQuery<DataResult<List<RequestGoodsSupplyDetail>>>;
