namespace Engineering.Application.Services.CostCenters.Models.CostCenterModels.Services;

public record CostCenterWarehouseServiceModel(
    long Id,
    long? WarehouseTypeId,
    long? ManagerId,
    string? ManagerContact,
    string? ManagerFullName,
    string? Code,
    string? Name,
    bool IsDefault
    );
