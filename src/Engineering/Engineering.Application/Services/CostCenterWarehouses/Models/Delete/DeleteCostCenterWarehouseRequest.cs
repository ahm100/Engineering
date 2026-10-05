namespace Engineering.Application.Services.CostCenterWarehouses.Models.Delete;

public record DeleteCostCenterWarehouseRequest(
    long CostCenterWarehouseId
     ) : IHttpRequest;
