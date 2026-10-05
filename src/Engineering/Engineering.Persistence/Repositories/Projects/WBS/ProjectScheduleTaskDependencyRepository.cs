using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Repositories.Projects.WBS;

public class ProjectScheduleTaskDependencyRepository : BaseRepository<EngineeringDBContext, ProjectScheduleTaskDependency>, IProjectScheduleTaskDependencyRepository
{
    public ProjectScheduleTaskDependencyRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectScheduleTaskDependency?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ProjectScheduleTaskDependency>?> GetByTaskIds(
        List<long> ids, CT ct)
    {
        return await DbSet
            .Where(x =>
                ids.Contains(x.SuccessorTaskId) ||
                ids.Contains(x.PredecessorTaskId))
            .ToListAsync(ct);
    }

    public async Task<List<ProjectScheduleTaskDependency>?> GetBySuccessorTaskIds(
        List<long> ids, CT ct)
    {
        return await DbSet
            .Where(x => ids.Contains(x.SuccessorTaskId) && !x.IsDeleted)
            .ToListAsync(ct);
    }
}