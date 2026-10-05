using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Repositories.Projects.WBS;

public class ProjectScheduleTaskOperationRepository : BaseRepository<EngineeringDBContext, ProjectScheduleTaskOperation>, IProjectScheduleTaskOperationRepository
{
    public ProjectScheduleTaskOperationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectScheduleTaskOperation?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ProjectScheduleTaskOperation>?> GetByTaskIds(
        List<long> ids, CT ct)
    {
        return await DbSet
            .Where(e => ids.Contains(e.ProjectScheduleTaskId))
            .ToListAsync(ct);
    }
}