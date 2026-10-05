namespace Engineering.Application.Services.CostCenters.Models.UpdateCostCenter;

public record UpdateCostCenterWarehouseModel(
    long? CostCenterWarehouseId,
    long Id,
    bool IsDefault,
    bool IsDeleted
     ) : IHttpRequest;
