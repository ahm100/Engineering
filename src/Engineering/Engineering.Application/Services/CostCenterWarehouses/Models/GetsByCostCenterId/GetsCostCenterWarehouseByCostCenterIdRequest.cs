namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsByCostCenterId;

public record GetsCostCenterWarehouseByCostCenterIdRequest(
    long CostCenterId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
