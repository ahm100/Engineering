namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseById;

public record GetProjectWarehouseByIdResponse(
    long Id,
    long ProjectId,
    long WarehouseId,
    long? WarehouseTypeId,
    long? ManagerId,
    string? ManagerContact,
    string? ManagerFullName,
    string? Code,
    string? Name,
    bool IsDefault);
