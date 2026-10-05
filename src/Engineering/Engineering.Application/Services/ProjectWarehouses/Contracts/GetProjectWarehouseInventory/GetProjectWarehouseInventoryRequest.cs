namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseInventory;

public record GetProjectWarehouseInventoryRequest(
    long ProjectId,
    long ProductId,
    string? FilterData,
    int PageIndex,
    int PageSize) : IHttpRequest;
