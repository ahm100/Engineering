
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetById;

public record GetCostCenterWarehouseByIdResponse(
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
