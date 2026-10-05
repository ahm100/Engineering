using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectWarehouses.Contracts;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectWarehouseRepository :
    BaseRepository<EngineeringDBContext, ProjectWarehouse>,
    IProjectWarehouseRepository
{
    public ProjectWarehouseRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectWarehouse?> GetProjectWarehouse(
        long id,
        CT ct)
    {
        return await DbSet
            .Include(oo => oo.Project)
                .ThenInclude(oo => oo.ProjectWarehouses)
            .FirstOrDefaultAsync(oo => oo.Id == id, ct);
    }

    public async Task<List<ProjectWarehouse>> GetProjectWarehouses(
        long projectId,
        CT ct)
    {
        return await DbSet
            .Where(oo => oo.ProjectId == projectId)
            .ToListAsync(ct);
    }

    public async Task<List<ProjectWarehouse>> GetProjectWarehousesByIds(
        List<long> ids,
        CT ct)
    {
        return await DbSet
            .Where(oo => ids.Contains(oo.Id))
            .ToListAsync(ct);
    }

    public async Task<ProjectWarehouseDataModel?> GetProjectWarehouseById(
        long id,
        CT ct)
    {
        return await DbSet
            .AsNoTracking()
            .Where(oo => oo.Id == id)
            .Select(oo => new ProjectWarehouseDataModel(
                oo.Id,
                oo.ProjectId,
                oo.WarehouseId,
                oo.IsDefault))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<(List<ProjectWarehouseModel> Data, int RowCount)> GetProjectWarehousesByProjectId(
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .AsNoTracking()
            .Where(oo => oo.ProjectId == projectId)
            .OrderByDescending(oo => oo.Created)
            .Select(oo => new ProjectWarehouseModel
            {
                ProjectWarehouseId = oo.Id,
                WarehouseId = oo.WarehouseId,
                IsDefault = oo.IsDefault
            });

        var rowCount = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return (await query.ToListAsync(ct), rowCount);
    }

    public async Task<ProjectWarehouseModel?> GetDefaultProjectWarehouseByProjectId(
        long projectId,
        CT ct)
    {
        return await DbSet
            .AsNoTracking()
            .Where(oo => oo.ProjectId == projectId && oo.IsDefault)
            .Select(oo => new ProjectWarehouseModel
            {
                ProjectWarehouseId = oo.Id,
                WarehouseId = oo.WarehouseId,
                IsDefault = oo.IsDefault
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<(List<ProjectWarehouseProjectModel> Data, int RowCount)> GetProjectWarehousesByProjectIds(
        List<long> projectIds,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .AsNoTracking()
            .Where(oo => projectIds.Contains(oo.ProjectId))
            .OrderByDescending(oo => oo.Created)
            .Select(oo => new ProjectWarehouseProjectModel(
                oo.Id,
                oo.ProjectId,
                oo.Project.ProjectCode,
                oo.Project.ProjectName,
                oo.WarehouseId,
                oo.IsDefault));

        var rowCount = await query.CountAsync(ct);
        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return (await query.ToListAsync(ct), rowCount);
    }

    public async Task<bool> ExistsProjectWarehouse(
        long projectId,
        long warehouseId,
        long? excludedId,
        CT ct)
    {
        return await DbSet.AnyAsync(
            oo => oo.ProjectId == projectId && oo.WarehouseId == warehouseId &&
                  (excludedId == null || oo.Id != excludedId),
            ct);
    }

    public async Task<List<long>> GetWarehouseIds(long projectId, CT ct)
    {
        return await DbSet
            .AsNoTracking()
            .Where(oo => oo.ProjectId == projectId)
            .Select(oo => oo.WarehouseId)
            .ToListAsync(ct);
    }

    public async Task CreateProjectWarehouse(ProjectWarehouse projectWarehouse, CT ct)
    {
        await Create(projectWarehouse, ct);
    }

    public async Task UpdateProjectWarehouse(ProjectWarehouse projectWarehouse, CT ct)
    {
        await Update(projectWarehouse);
    }
}
