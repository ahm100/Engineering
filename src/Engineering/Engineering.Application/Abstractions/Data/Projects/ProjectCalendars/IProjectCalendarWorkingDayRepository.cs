using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;

public interface IProjectCalendarWorkingDayRepository : IBaseRepository<ProjectCalendarWorkingDay>
{
    Task<ProjectCalendarWorkingDay?> GetById(
        long id, CT ct);

}