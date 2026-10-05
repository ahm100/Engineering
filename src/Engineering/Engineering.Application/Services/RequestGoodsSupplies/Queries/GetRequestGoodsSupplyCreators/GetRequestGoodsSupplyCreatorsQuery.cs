namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyCreators;

public record GetRequestGoodsSupplyCreatorsQuery(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? RequestGoodsSupplyIds
    ) : IQuery<DataResult<List<long>>>;
