namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseAssets;

public record GetsCostCenterWarehouseAssetsRequest(
    long CostCenterId,
    long? ProductGroupId,
    long? ProductId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
