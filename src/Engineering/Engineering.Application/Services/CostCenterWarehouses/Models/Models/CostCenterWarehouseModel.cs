namespace Engineering.Application.Services.CostCenterWarehouses.Models.Models;

public record CostCenterWarehouseModel(
    long CostCenterWarehouseId,
    long Id,
    long? WarehouseTypeId,
    long? ManagerId,
    string? ManagerContact,
    string? ManagerFullName,
    string? Code,
    string? Name,
    bool IsDefault
    );
