namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyProducts;

public record GetRequestGoodsSupplyProductsQuery(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? RequestGoodsSupplyIds,
    List<long>? ProductGroupIds
    ) : IQuery<DataResult<List<long>>>;
