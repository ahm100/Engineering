using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;

public interface IProjectCalendarExceptionRepository : IBaseRepository<ProjectCalendarException>
{
    Task<ProjectCalendarException?> GetById(
        long id, CT ct);

}