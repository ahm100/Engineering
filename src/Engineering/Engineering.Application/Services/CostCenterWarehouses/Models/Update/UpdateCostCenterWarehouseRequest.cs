namespace Engineering.Application.Services.CostCenterWarehouses.Models.Update;

public record UpdateCostCenterWarehouseRequest(
    long CostCenterWarehouseId,
    long Id,
    bool IsDefault
     ) : IHttpRequest;
