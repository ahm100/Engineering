namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseAssets;

public record GetProjectWarehouseAssetsResponse(
    List<GetProjectWarehouseAssetsModel> Data,
    int RowCount);
