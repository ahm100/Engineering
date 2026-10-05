namespace Engineering.Application.Services.ProjectWarehouses.Contracts.SaveProjectWarehouses;

public record SaveProjectWarehouseModel(
    long? Id,
    long WarehouseId,
    bool IsDefault,
    bool? IsDeleted);
