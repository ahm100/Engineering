namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseInventory;

public record GetsCostCenterWarehouseInventoryRequest(
    long CostCenterId,
    long ProductId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
