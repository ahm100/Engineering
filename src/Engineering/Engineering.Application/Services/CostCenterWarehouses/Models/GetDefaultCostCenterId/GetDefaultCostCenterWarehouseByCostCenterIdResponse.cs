
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetDefaultCostCenterId;

public record GetDefaultCostCenterWarehouseByCostCenterIdResponse(
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
