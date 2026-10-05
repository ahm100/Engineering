namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseByCostCenterIds;

public record GetsCostCenterWarehouseByCostCenterIdsRequest(
    List<long> CostCenterIds,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
