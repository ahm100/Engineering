namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectIds;

public record GetProjectWarehousesByProjectIdsModel(
    long ProjectId,
    string? ProjectCode,
    string ProjectName,
    List<ProjectWarehouseModel> Warehouses);
