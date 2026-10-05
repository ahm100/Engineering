namespace Engineering.Application.Services.ProjectWarehouses.Contracts.CreateProjectWarehouse;

public record CreateProjectWarehouseRequest(
    long ProjectId,
    long WarehouseId,
    bool IsDefault) : IHttpRequest;
