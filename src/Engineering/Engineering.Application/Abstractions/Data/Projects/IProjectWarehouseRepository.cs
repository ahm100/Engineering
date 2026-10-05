using Engineering.Application.Services.ProjectWarehouses.Contracts;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectWarehouseRepository : IBaseRepository<ProjectWarehouse>
{
    Task<ProjectWarehouse?> GetProjectWarehouse(long id, CT ct);

    Task<List<ProjectWarehouse>> GetProjectWarehouses(long projectId, CT ct);

    Task<List<ProjectWarehouse>> GetProjectWarehousesByIds(List<long> ids, CT ct);

    Task<ProjectWarehouseDataModel?> GetProjectWarehouseById(long id, CT ct);

    Task<(List<ProjectWarehouseModel> Data, int RowCount)> GetProjectWarehousesByProjectId(
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectWarehouseProjectModel> Data, int RowCount)> GetProjectWarehousesByProjectIds(
        List<long> projectIds,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<ProjectWarehouseModel?> GetDefaultProjectWarehouseByProjectId(
        long projectId,
        CT ct);

    Task<bool> ExistsProjectWarehouse(long projectId, long warehouseId, long? excludedId, CT ct);

    Task<List<long>> GetWarehouseIds(long projectId, CT ct);

    Task CreateProjectWarehouse(ProjectWarehouse projectWarehouse, CT ct);

    Task UpdateProjectWarehouse(ProjectWarehouse projectWarehouse, CT ct);
}
