using Engineering.Application.Services.ProjectWarehouses.Contracts.CreateProjectWarehouse;
using Engineering.Application.Services.ProjectWarehouses.Contracts.DeleteProjectWarehouse;
using Engineering.Application.Services.ProjectWarehouses.Contracts.DeleteProjectWarehouses;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetDefaultProjectWarehouse;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseAssets;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseById;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseInventory;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectId;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectIds;
using Engineering.Application.Services.ProjectWarehouses.Contracts.SaveProjectWarehouses;
using Engineering.Application.Services.ProjectWarehouses.Contracts.UpdateProjectWarehouse;

namespace Engineering.Application.Services.ProjectWarehouses;

public interface IProjectWarehouseLogic
{
    Task<Result<CreateProjectWarehouseResponse?>> CreateProjectWarehouse(CreateProjectWarehouseRequest request, CT ct);
    Task<Result<SaveProjectWarehousesResponse?>> SaveProjectWarehouses(SaveProjectWarehousesRequest request, CT ct);
    Task<Result<UpdateProjectWarehouseResponse?>> UpdateProjectWarehouse(UpdateProjectWarehouseRequest request, CT ct);
    Task<Result<DeleteProjectWarehouseResponse?>> DeleteProjectWarehouse(DeleteProjectWarehouseRequest request, CT ct);
    Task<Result<DeleteProjectWarehousesResponse?>> DeleteProjectWarehouses(DeleteProjectWarehousesRequest request, CT ct);
    Task<Result<GetProjectWarehouseByIdResponse?>> GetProjectWarehouseById(GetProjectWarehouseByIdRequest request, CT ct);
    Task<Result<GetProjectWarehousesByProjectIdResponse?>> GetProjectWarehousesByProjectId(
        GetProjectWarehousesByProjectIdRequest request,
        CT ct);
    Task<Result<GetProjectWarehousesByProjectIdsResponse?>> GetProjectWarehousesByProjectIds(
        GetProjectWarehousesByProjectIdsRequest request,
        CT ct);
    Task<Result<GetDefaultProjectWarehouseResponse?>> GetDefaultProjectWarehouse(
        GetDefaultProjectWarehouseRequest request,
        CT ct);
    Task<Result<GetProjectWarehouseInventoryResponse?>> GetProjectWarehouseInventory(
        GetProjectWarehouseInventoryRequest request,
        CT ct);
    Task<Result<GetProjectWarehouseAssetsResponse?>> GetProjectWarehouseAssets(
        GetProjectWarehouseAssetsRequest request,
        CT ct);
}
