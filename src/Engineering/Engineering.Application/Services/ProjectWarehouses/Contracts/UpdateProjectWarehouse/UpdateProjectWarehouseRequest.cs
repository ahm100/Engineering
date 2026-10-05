namespace Engineering.Application.Services.ProjectWarehouses.Contracts.UpdateProjectWarehouse;

public record UpdateProjectWarehouseRequest(
    long Id,
    long WarehouseId,
    bool IsDefault) : IHttpRequest;
