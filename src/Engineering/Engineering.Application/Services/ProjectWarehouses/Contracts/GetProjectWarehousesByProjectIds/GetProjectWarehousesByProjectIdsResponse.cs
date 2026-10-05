namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectIds;

public record GetProjectWarehousesByProjectIdsResponse(
    List<GetProjectWarehousesByProjectIdsModel> Data,
    int RowCount);
