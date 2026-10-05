namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectId;

public record GetProjectWarehousesByProjectIdResponse(
    List<ProjectWarehouseModel> Data,
    int RowCount);
