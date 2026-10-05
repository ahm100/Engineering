namespace Engineering.Application.Services.CostCenters.Models.CostCenterModels.Services;

public record UpdateCostCenterWarehouseServiceModel(
    long? CostCenterWarehouseId,
    long? WarehouseId,
    long? WarehouseTypeId,
    long? ManagerId,
    string? ManagerContact,
    string? ManagerFullName,
    string? Code,
    string? Name,
    bool IsDefault
    );
