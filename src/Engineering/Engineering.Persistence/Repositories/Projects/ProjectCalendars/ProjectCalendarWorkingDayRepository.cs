using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Domain.Entities.Projects.ProjectCalendars;


namespace Engineering.Persistence.Repositories.Projects.ProjectCalendars;

public class ProjectCalendarWorkingDayRepository : BaseRepository<EngineeringDBContext, ProjectCalendarWorkingDay>, IProjectCalendarWorkingDayRepository
{
    public ProjectCalendarWorkingDayRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectCalendarWorkingDay?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

}