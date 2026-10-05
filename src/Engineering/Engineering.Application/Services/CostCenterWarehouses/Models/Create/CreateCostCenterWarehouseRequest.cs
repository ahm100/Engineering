namespace Engineering.Application.Services.CostCenterWarehouses.Models.Create;

public record CreateCostCenterWarehouseRequest(
    long CostCenterId,
    long Id,
    bool IsDefault
     ) : IHttpRequest;
