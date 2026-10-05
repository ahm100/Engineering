using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Domain.Entities.Projects.ProjectCalendars;


namespace Engineering.Persistence.Repositories.Projects.ProjectCalendars;

public class ProjectCalendarRepository : BaseRepository<EngineeringDBContext, ProjectCalendar>, IProjectCalendarRepository
{
    public ProjectCalendarRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectCalendar?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .Include(c => c.ProjectCalendarWorkingDaies)
                .ThenInclude(d => d.ProjectCalendarWorkingTimes)
            .Include(c => c.ProjectCalendarExceptions)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ProjectCalendar>?> GetByProjectId(
        long projectId, CT ct)
    {
        return await DbSet
            .Include(x => x.ProjectCalendarWorkingDaies)
            .ThenInclude(x => x.ProjectCalendarWorkingTimes)
            .Include(x => x.ProjectCalendarExceptions)

            .Where(x => x.ProjectId == projectId)
            .ToListAsync(ct);
    }

}