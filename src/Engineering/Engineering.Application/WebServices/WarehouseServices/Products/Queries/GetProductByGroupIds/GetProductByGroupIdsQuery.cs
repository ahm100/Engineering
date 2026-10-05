using Engineering.Application.WebServices.WarehouseServices.Products.Models.GetProductByGroupIds;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Queries.GetProductByGroupIds;

public record GetProductByGroupIdsQuery(
    List<long> GroupIds,
    string? FilterData
    ) : IQuery<DataResult<List<GetProductsByGroupIdsModel>>>;