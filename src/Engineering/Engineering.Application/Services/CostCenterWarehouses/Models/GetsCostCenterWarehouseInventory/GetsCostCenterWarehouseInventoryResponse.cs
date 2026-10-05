
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseInventory;

public record GetsCostCenterWarehouseInventoryResponse(
    List<GetsCostCenterWarehouseInventoryResponseModel> Data,
    int RowCount);
