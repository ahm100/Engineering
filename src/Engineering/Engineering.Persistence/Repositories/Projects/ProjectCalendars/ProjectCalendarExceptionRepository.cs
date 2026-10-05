using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Domain.Entities.Projects.ProjectCalendars;


namespace Engineering.Persistence.Repositories.Projects.ProjectCalendars;

public class ProjectCalendarExceptionRepository : BaseRepository<EngineeringDBContext, ProjectCalendarException>, IProjectCalendarExceptionRepository
{
    public ProjectCalendarExceptionRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectCalendarException?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

}