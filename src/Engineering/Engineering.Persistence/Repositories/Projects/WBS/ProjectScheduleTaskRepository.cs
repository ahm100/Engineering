using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Repositories.Projects.WBS;

public class ProjectScheduleTaskRepository : BaseRepository<EngineeringDBContext, ProjectScheduleTask>, IProjectScheduleTaskRepository
{
    public ProjectScheduleTaskRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectScheduleTask?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ProjectScheduleTask>?> GetByProjectScheduleImportId(
        long id, CT ct)
    {
        return await DbSet
            .Where(x => x.ProjectScheduleImportId == id)
            .ToListAsync(ct);
    }

    public async Task<bool> HasChildren(long id, CT ct)
        => await DbSet.AnyAsync(e => e.ParentId==id, ct);

    public async Task<int> GetMaxMppUid(
        long importId, CT ct)
    {
        return await DbSet.IgnoreQueryFilters()
            .Where(x => x.ProjectScheduleImportId == importId)
            .MaxAsync(x => (int?)x.MppUid, ct) ?? 0;
    }

    public async Task<int> GetMaxMppId(
        long importId, CT ct)
    {
        return await DbSet.IgnoreQueryFilters()
            .Where(x => x.ProjectScheduleImportId == importId)
            .MaxAsync(x => (int?)x.MppId, ct) ?? 0;
    }
}