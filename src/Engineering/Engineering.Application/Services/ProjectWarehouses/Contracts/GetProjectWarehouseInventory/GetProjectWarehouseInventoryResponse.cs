namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseInventory;

public record GetProjectWarehouseInventoryResponse(
    List<GetProjectWarehouseInventoryModel> Data,
    int RowCount);
