namespace Engineering.Application.Services.CostCenterWarehouses.Models.Creates;

public record CreatesCostCenterWarehouseRequest(
    long CostCenterId,
    List<CostCenterWarehousesModel> CostCenterWarehouses
     ) : IHttpRequest;

public record CostCenterWarehousesModel(
    long? Id,
    long WarehouseId,
    bool IsDefault,
    bool? IsDeleted
    );