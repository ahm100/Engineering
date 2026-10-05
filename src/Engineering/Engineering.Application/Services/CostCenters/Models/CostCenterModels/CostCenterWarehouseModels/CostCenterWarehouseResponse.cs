
namespace Engineering.Application.Services.CostCenters.Models.CostCenterModels.CostCenterWarehouseModels;

public record CostCenterWarehouseResponse(
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
