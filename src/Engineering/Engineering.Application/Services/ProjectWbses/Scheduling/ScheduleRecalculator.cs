using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Abstractions.Data.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Scheduling;

public class ScheduleRecalculator
{
    private readonly IProjectScheduleImportRepository _importRepository;
    private readonly IProjectScheduleTaskRepository _taskRepository;
    private readonly IProjectScheduleTaskDependencyRepository _dependencyRepository;
    private readonly IProjectCalendarRepository _calendarRepository;

    public ScheduleRecalculator(
        IProjectScheduleImportRepository importRepository,
        IProjectScheduleTaskRepository taskRepository,
        IProjectScheduleTaskDependencyRepository dependencyRepository,
        IProjectCalendarRepository calendarRepository)
    {
        _importRepository = importRepository;
        _taskRepository = taskRepository;
        _dependencyRepository = dependencyRepository;
        _calendarRepository = calendarRepository;
    }

    public async Task<Result> RecalculateProjectAsync(long projectId, CT ct)
    {
        var import = await _importRepository.GetByProjectId(projectId, ct);
        if (import is null) 
            return Result.Failure(ProjectErrors.ProjectNotFound);

        var tasks = await _taskRepository.GetByProjectScheduleImportId(import.Id, ct) ?? [];
        var deps = await _dependencyRepository.GetByTaskIds(tasks.Select(t => t.Id).ToList(), ct) ?? [];
        var cals = await _calendarRepository.GetByProjectId(import.ProjectId, ct) ?? [];

        var calendar = cals.FirstOrDefault(c => c.IsDefault) ?? cals.FirstOrDefault();
        if (calendar is null) 
            return Result.Failure(ProjectErrors.CalendarNotFound);

        try
        {
            ScheduleCalculator.Recalculate(tasks, deps, WorkCalendar.From(calendar),
                import.ScheduleStartDate ?? DateTime.Today, import.StatusDate);
        }
        catch (InvalidOperationException)
        {
            return Result.Failure(ProjectErrors.InvalidCalendarDefinition);
        }
        foreach (var t in tasks)
            await _taskRepository.Update(t);

        return Result.Success();
    }
}