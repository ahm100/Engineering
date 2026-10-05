namespace Engineering.Application.Services.ProjectWarehouses.Contracts;

public record ProjectWarehouseDataModel(
    long ProjectWarehouseId,
    long ProjectId,
    long WarehouseId,
    bool IsDefault);
