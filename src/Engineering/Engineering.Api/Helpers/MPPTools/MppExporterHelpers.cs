using Aspose.Tasks;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using AsposeCalendar = Aspose.Tasks.Calendar;
using AsposeTask = Aspose.Tasks.Task;

namespace Engineering.Api.Helpers.MppTools;

public static class MppExporterHelpers
{
    public static List<AsposeCalendar> BuildCalendars(
        Project project,
        IReadOnlyCollection<MppCalendarModel> calendars)
    {
        var result = new List<AsposeCalendar>();
        AsposeCalendar? defaultCalendar = null;

        foreach (var calendarModel in calendars)
        {
            var calendar = project.Calendars.Add(calendarModel.Name);

            foreach (var dayModel in calendarModel.WorkingDays)
            {
                if (!TryMapDayOfWeek(
                        dayModel.DayOfWeek,
                        out var dayType))
                {
                    continue;
                }

                var weekDay = calendar.WeekDays
                    .FirstOrDefault(w => w.DayType == dayType);

                if (weekDay is null)
                {
                    weekDay = new WeekDay(dayType);
                    calendar.WeekDays.Add(weekDay);
                }

                weekDay.DayWorking = dayModel.IsWorking;

                if (dayModel.IsWorking)
                {
                    weekDay.WorkingTimes.Clear();

                    var validTimes = dayModel.WorkingTimes
                        .Where(wt => wt.To > wt.From)
                        .ToList();

                    if (validTimes.Count == 0)
                    {
                        weekDay.WorkingTimes.Add(
                            new WorkingTime(
                                DateTime.Today.AddHours(8),
                                DateTime.Today.AddHours(17)));
                    }
                    else
                    {
                        foreach (var wt in validTimes)
                        {
                            weekDay.WorkingTimes.Add(
                                new WorkingTime(
                                    DateTime.Today.Add(wt.From),
                                    DateTime.Today.Add(wt.To)));
                        }
                    }
                }
            }

            foreach (var exceptionModel in calendarModel.Exceptions)
            {
                var exception = new CalendarException
                {
                    FromDate = exceptionModel.Date,
                    ToDate = exceptionModel.Date,
                    DayWorking = exceptionModel.IsWorking,
                    Name = exceptionModel.Description ?? string.Empty
                };

                if (exceptionModel.IsWorking &&
                    exceptionModel.From.HasValue &&
                    exceptionModel.To.HasValue)
                {
                    exception.WorkingTimes.Add(
                        new WorkingTime(
                            exceptionModel.Date.Add(
                                exceptionModel.From.Value),
                            exceptionModel.Date.Add(
                                exceptionModel.To.Value)));
                }

                calendar.Exceptions.Add(exception);
            }

            result.Add(calendar);

            if (calendarModel.IsDefault)
                defaultCalendar = calendar;
        }

        if (defaultCalendar is not null)
            project.Set(Prj.Calendar, defaultCalendar);

        return result;
    }

    public static Dictionary<int, AsposeTask> BuildWbsTasks(
        Project project,
        IReadOnlyCollection<MppTaskModel> tasks)
    {
        var result = new Dictionary<int, AsposeTask>();

        var summaryTasks = tasks
            .Where(x => x.IsSummary)
            .OrderBy(x => x.OutlineLevel)
            .ThenBy(x => x.SortOrder)
            .ToList();

        foreach (var taskModel in summaryTasks)
        {
            var parentTask =
                taskModel.ParentUid.HasValue &&
                result.TryGetValue(
                    taskModel.ParentUid.Value,
                    out var p)
                    ? p
                    : null;

            var siblings =
                parentTask?.Children ??
                project.RootTask.Children;

            var task = siblings.Add(taskModel.Name);

            result.Add(taskModel.Uid ?? 0, task);
        }

        return result;
    }

    public static Dictionary<int, AsposeTask> BuildActivityTasks(
        Project project,
        IReadOnlyCollection<MppTaskModel> tasks,
        IReadOnlyDictionary<int, AsposeTask> wbsMap,
        IReadOnlyCollection<AsposeCalendar> calendars)
    {
        var result = new Dictionary<int, AsposeTask>();

        var activityTasks = tasks
            .Where(x => !x.IsSummary)
            .OrderBy(x => x.SortOrder)
            .ToList();

        foreach (var taskModel in activityTasks)
        {
            var parentTask =
                taskModel.ParentUid.HasValue &&
                wbsMap.TryGetValue(
                    taskModel.ParentUid.Value,
                    out var wt)
                    ? wt
                    : null;

            var siblings =
                parentTask?.Children ??
                project.RootTask.Children;

            var task = siblings.Add(taskModel.Name);

            if (taskModel.CalendarUid.HasValue)
            {
                var cal = calendars.FirstOrDefault(
                    x => x.Uid == taskModel.CalendarUid.Value);

                if (cal is not null)
                    task.Calendar = cal;
            }

            if (taskModel.Start.HasValue)
                task.Start = taskModel.Start.Value;

            if (taskModel.Finish.HasValue)
                task.Finish = taskModel.Finish.Value;

            if (taskModel.DurationMinutes.HasValue)
            {
                task.Duration = project.GetDuration(
                    taskModel.DurationMinutes.Value,
                    TimeUnitType.Minute);
            }

            task.PercentComplete =
                (int)(taskModel.PercentComplete ?? 0);

            task.IsMilestone =
                taskModel.IsMilestone;

            if (taskModel.ActualStart.HasValue)
                task.ActualStart =
                    taskModel.ActualStart.Value;

            if (taskModel.ActualFinish.HasValue)
                task.ActualFinish =
                    taskModel.ActualFinish.Value;

            if (taskModel.ActualDurationMinutes.HasValue)
            {
                task.ActualDuration = project.GetDuration(
                    taskModel.ActualDurationMinutes.Value,
                    TimeUnitType.Minute);
            }

            result.Add(
                taskModel.Uid ?? 0,
                task);
        }

        return result;
    }

    public static void BuildDependencies(
        Project project,
        IReadOnlyCollection<MppDependencyModel> dependencies,
        IReadOnlyDictionary<int, AsposeTask> taskMap)
    {
        foreach (var dependencyModel in dependencies)
        {
            if (!taskMap.TryGetValue(
                    dependencyModel.PredecessorUid ?? 0,
                    out var pred))
            {
                continue;
            }

            if (!taskMap.TryGetValue(
                    dependencyModel.SuccessorUid ?? 0,
                    out var succ))
            {
                continue;
            }

            var lag = project.GetDuration(
                dependencyModel.LagMinutes,
                TimeUnitType.Minute);

            project.TaskLinks.Add(
                pred,
                succ,
                MapLinkType(dependencyModel.Type),
                lag);
        }
    }

    public static void BuildBaselines(
        Project project,
        IReadOnlyCollection<MppTaskModel> tasks,
        IReadOnlyDictionary<int, AsposeTask> taskMap)
    {
        var baselineTasks = tasks
            .Where(x =>
                !x.IsSummary &&
                (
                    x.BaselineStart.HasValue ||
                    x.BaselineFinish.HasValue ||
                    x.BaselineDurationMinutes.HasValue
                ))
            .Select(x =>
                taskMap.TryGetValue(
                    x.Uid ?? 0,
                    out var task)
                    ? task
                    : null)
            .Where(x => x is not null)
            .Cast<AsposeTask>()
            .ToList();

        if (baselineTasks.Count == 0)
            return;

        project.SetBaseline(
            BaselineType.Baseline,
            baselineTasks);
    }

    private static bool TryMapDayOfWeek(
        DayOfWeek dayOfWeek,
        out DayType dayType)
    {
        switch (dayOfWeek)
        {
            case DayOfWeek.Sunday:
                dayType = DayType.Sunday;
                return true;

            case DayOfWeek.Monday:
                dayType = DayType.Monday;
                return true;

            case DayOfWeek.Tuesday:
                dayType = DayType.Tuesday;
                return true;

            case DayOfWeek.Wednesday:
                dayType = DayType.Wednesday;
                return true;

            case DayOfWeek.Thursday:
                dayType = DayType.Thursday;
                return true;

            case DayOfWeek.Friday:
                dayType = DayType.Friday;
                return true;

            case DayOfWeek.Saturday:
                dayType = DayType.Saturday;
                return true;

            default:
                dayType = default;
                return false;
        }
    }

    private static TaskLinkType MapLinkType(
        MppDependencyType type)
    {
        return type switch
        {
            MppDependencyType.FinishToStart =>
                TaskLinkType.FinishToStart,

            MppDependencyType.StartToStart =>
                TaskLinkType.StartToStart,

            MppDependencyType.FinishToFinish =>
                TaskLinkType.FinishToFinish,

            MppDependencyType.StartToFinish =>
                TaskLinkType.StartToFinish,

            _ => throw new ArgumentOutOfRangeException(
                nameof(type),
                type,
                "نوع وابستگی نامعتبر است.")
        };
    }
}