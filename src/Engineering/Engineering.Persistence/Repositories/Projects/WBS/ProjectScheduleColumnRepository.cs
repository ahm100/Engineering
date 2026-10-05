using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Repositories.Projects.WBS;

public class ProjectScheduleColumnRepository : BaseRepository<EngineeringDBContext, ProjectScheduleColumn>, IProjectScheduleColumnRepository
{
    public ProjectScheduleColumnRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<ProjectScheduleColumn>> GetByImportId(
        long importId, CT ct)
    {
        return await DbSet
            .Where(x => x.ProjectScheduleImportId == importId)
            .OrderBy(x => x.SortOrder)
            .ToListAsync(ct);
    }

    public async Task<ProjectScheduleColumn?> GetById(
        long id, CT ct)
        => await DbSet
            .FirstOrDefaultAsync(e => e.Id == id, ct);
}
