namespace Engineering.Application.Services.CostCenterWarehouses.Models.Delete;

public record DeleteCostCenterWarehouseResponse(
    long CostCenterWarehouseId,
    bool IsDeleted
    );
