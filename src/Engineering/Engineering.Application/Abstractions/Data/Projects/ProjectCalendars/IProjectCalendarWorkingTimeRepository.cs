using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;

public interface IProjectCalendarWorkingTimeRepository : IBaseRepository<ProjectCalendarWorkingTime>
{
    Task<ProjectCalendarWorkingTime?> GetById(
        long id, CT ct);

}