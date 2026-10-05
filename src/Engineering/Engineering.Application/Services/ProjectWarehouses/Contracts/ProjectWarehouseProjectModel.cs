namespace Engineering.Application.Services.ProjectWarehouses.Contracts;

public record ProjectWarehouseProjectModel(
    long ProjectWarehouseId,
    long ProjectId,
    string? ProjectCode,
    string ProjectName,
    long WarehouseId,
    bool IsDefault);
