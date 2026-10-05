
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseAssets;

public record GetsCostCenterWarehouseAssetsResponse(
    List<GetsCostCenterWarehouseAssetsResponseModel> Data,
    int RowCount);
