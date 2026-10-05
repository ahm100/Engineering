using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;

public interface IProjectCalendarRepository : IBaseRepository<ProjectCalendar>
{
    Task<ProjectCalendar?> GetById(
        long id, CT ct);

    Task<List<ProjectCalendar>?> GetByProjectId(
        long projectId, CT ct);

}