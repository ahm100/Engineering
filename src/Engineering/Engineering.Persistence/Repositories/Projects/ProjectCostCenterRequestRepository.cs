using Engineering.Application.Abstractions.Data.ProjectCostCenterRequests;
using Engineering.Application.Services.ProjectCostCenterRequests.Models;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.ProjectCostCenterRequests;

namespace Engineering.Persistence.Repositories.Projects;
public class ProjectCostCenterRequestRepository : BaseRepository<EngineeringDBContext, ProjectCostCenterRequest>, IProjectCostCenterRequestRepository
{
    public ProjectCostCenterRequestRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectCostCenterRequest?> GetById(long id, CT ct)
    {
        return await DbSet
            .Include(x => x.Project)
                .ThenInclude(p => p!.ProjectCostCenters)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ProjectCostCenterRequest>> GetByProjectId(long projectId, CT ct)
    {
        return await DbSet
            .Where(x => x.ProjectId == projectId)
            .OrderByDescending(x => x.Created)
            .ToListAsync(ct);
    }

    public async Task<List<ProjectCostCenterRequest>> GetPendingByProjectId(long projectId, CT ct)
    {
        return await DbSet
            .Where(x => x.ProjectId == projectId && x.Status == ProjectCostCenterRequestStatus.InProgress)
            .OrderByDescending(x => x.Created)
            .ToListAsync(ct);
    }

    public async Task<(List<ProjectCostCenterRequestModel> Data, int RowCount)> GetRequests(
    long? projectId,
    ProjectCostCenterRequestStatus? status,
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var query = DbSet.Where(x => !x.IsDeleted);

        if (projectId.HasValue)
            query = query.Where(x => x.ProjectId == projectId.Value);

        if (status.HasValue)
            query = query.Where(x => x.Status == status.Value);

        query = query.OrderByDescending(x => x.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await ProjectToModel(query).ToListAsync(ct);
        return (items, count);
    }

    public async Task<ProjectCostCenterRequestModel?> GetModelById(long id, CT ct)
    {
        return await ProjectToModel(DbSet)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    private static IQueryable<ProjectCostCenterRequestModel> ProjectToModel(
        IQueryable<ProjectCostCenterRequest> query)
    {
        return query.Select(x => new ProjectCostCenterRequestModel
        {
            Id = x.Id,
            ProjectId = x.ProjectId,
            ProjectName = x.Project.ProjectName,
            CityId = x.Project.CityId,
            RequestedCostCenterName = x.RequestedCostCenterName,
            Description = x.Description,
            Status = x.Status,
            CostCenterId = x.CostCenterId,
            CostCenterName = x.CostCenter != null
                ? x.CostCenter.CostCenterName
                : null,
            RejectionReason = x.RejectionReason,
            CreatorId = x.CreatorId,
            UpdaterId = x.UpdaterId,
            Created = x.Created,
            Updated = x.Updated
        });
    }
}