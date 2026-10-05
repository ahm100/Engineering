using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Repositories.Projects.WBS;

public class ProjectScheduleTaskValueRepository : BaseRepository<EngineeringDBContext, ProjectScheduleTaskValue>, IProjectScheduleTaskValueRepository
{
    public ProjectScheduleTaskValueRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectScheduleTaskValue?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ProjectScheduleTaskValue>?> GetByColumnId(
        long columnId, CT ct)
    {
        var query = DbSet.Where(e => e.ProjectScheduleColumnId == columnId);

        return await query.ToListAsync();
    }

    public async Task<ProjectScheduleTaskValue?> GetByTaskAndColumnId(
        long columnId, long taskId, CT ct)
        => await DbSet
            .FirstOrDefaultAsync(e => e.ProjectScheduleColumnId == columnId && e.ProjectScheduleTaskId == taskId);

    public async Task<List<ProjectScheduleTaskValue>> GetByImportId(
        long importId, CT ct)
        => await DbSet.Where(e => e.ProjectScheduleColumn.ProjectScheduleImportId == importId).ToListAsync(ct);
}
