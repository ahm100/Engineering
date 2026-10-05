namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetDefaultProjectWarehouse;

public record GetDefaultProjectWarehouseRequest(long ProjectId) : IHttpRequest;
