using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Domain.Entities.Projects.ProjectCalendars;


namespace Engineering.Persistence.Repositories.Projects.ProjectCalendars;

public class ProjectCalendarWorkingTimeRepository : BaseRepository<EngineeringDBContext, ProjectCalendarWorkingTime>, IProjectCalendarWorkingTimeRepository
{
    public ProjectCalendarWorkingTimeRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectCalendarWorkingTime?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

}