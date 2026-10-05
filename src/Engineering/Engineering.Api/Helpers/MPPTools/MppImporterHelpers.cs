using Aspose.Tasks;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Entities.Projects.Enums;
using AsposeCalendar = Aspose.Tasks.Calendar;
using AsposeTask = Aspose.Tasks.Task;

namespace Engineering.Api.Helpers.MppTools;

public static class MppImporterHelpers
{
    public static List<MppTaskModel> ParseTasks(
        Project project,
        IReadOnlyDictionary<string, MppCustomColumnModel> columnsByFieldId,
        CT ct)
    {
        var result = new List<MppTaskModel>();

        var tasks = project.EnumerateAllChildTasks()
            .Where(x => x != project.RootTask && !x.Get(Tsk.IsNull))   // skip blank rows
            .ToList();

        foreach (var task in tasks)
        {
            ct.ThrowIfCancellationRequested();

            var baseline = GetDefaultBaseline(task);
            var note = task.Get(Tsk.NotesText);

            result.Add(new MppTaskModel
            {
                Id = task.Id,
                Uid = task.Uid,
                Name = task.Name ?? string.Empty,
                SortOrder = task.Id,
                OutlineLevel = task.OutlineLevel,
                OutlineNumber = task.OutlineNumber,
                ParentUid = GetParentUid(task),
                CalendarUid = task.Calendar?.Uid,
                IsSummary = task.IsSummary || task.Children.Count > 0,
                IsMilestone = task.IsMilestone,
                IsCritical = task.IsCritical,
                Start = NormalizeDate(task.Start),
                Finish = NormalizeDate(task.Finish),
                DurationMinutes = ToMinutes(task.Duration),
                PercentComplete = task.PercentComplete,
                BaselineStart = NormalizeDate(baseline?.Start),
                BaselineFinish = NormalizeDate(baseline?.Finish),
                BaselineDurationMinutes = ToMinutes(baseline?.Duration),
                ActualStart = NormalizeDate(task.ActualStart),
                ActualFinish = NormalizeDate(task.ActualFinish),
                ActualDurationMinutes = ToMinutes(task.ActualDuration),
                IsManual = task.IsManual,
                RemainingDurationMinutes = ToMinutes(task.Get(Tsk.RemainingDuration)),
                PhysicalPercentComplete = task.Get(Tsk.PhysicalPercentComplete),
                Deadline = NormalizeDate(task.Get(Tsk.Deadline)),
                Cost = task.Get(Tsk.Cost),
                Note = string.IsNullOrWhiteSpace(note) ? null : note,
                IsEstimated = task.Get(Tsk.IsEstimated),
                CustomValues = ParseCustomValues(task, columnsByFieldId)
            });
        }
        return result;
    }

    public static List<MppCustomColumnModel> ParseCustomColumns(Project project)
    {
        var result = new List<MppCustomColumnModel>();

        foreach (var def in project.ExtendedAttributes)
        {
            if (def.ElementType != ElementType.Task) continue;
            if (!TryMapCustomType(def.CfType, out var type)) continue;

            result.Add(new MppCustomColumnModel
            {
                FieldId = def.FieldId,
                Code = def.FieldName,   // "Text1", "Number2", ...
                Alias = string.IsNullOrWhiteSpace(def.Alias) ? null : def.Alias.Trim(),
                DataType = type
            });
        }
        return result;
    }

    private static bool TryMapCustomType(CustomFieldType t, out ProjectScheduleColumnDataType type)
    {
        switch (t)
        {
            case CustomFieldType.Text:
                type = ProjectScheduleColumnDataType.String; return true;
            case CustomFieldType.Number:
            case CustomFieldType.Cost:
                type = ProjectScheduleColumnDataType.Decimal; return true;
            case CustomFieldType.Date:
            case CustomFieldType.Start:
            case CustomFieldType.Finish:
                type = ProjectScheduleColumnDataType.DateTime; return true;
            default:
                type = default; return false;   // Duration, Flag, outline codes, ...
        }
    }

    public static List<MppCustomValueModel> ParseCustomValues(
        AsposeTask task, IReadOnlyDictionary<string, MppCustomColumnModel> byFieldId)
    {
        var result = new List<MppCustomValueModel>();

        foreach (var attr in task.ExtendedAttributes)
        {
            if (!byFieldId.TryGetValue(attr.FieldId, out var col)) continue;

            var v = new MppCustomValueModel { ColumnCode = col.Code };
            switch (col.DataType)
            {
                case ProjectScheduleColumnDataType.String: v.StringValue = attr.TextValue; break;
                case ProjectScheduleColumnDataType.Decimal: v.DecimalValue = attr.NumericValue; break;
                case ProjectScheduleColumnDataType.DateTime: v.DateTimeValue = NormalizeDate(attr.DateValue); break;
            }
            if (v.HasValue) result.Add(v);
        }
        return result;
    }
    public static List<MppDependencyModel> ParseDependencies(
        Project project,
        CT ct)
    {
        var result = new List<MppDependencyModel>();

        foreach (var link in project.TaskLinks)
        {
            ct.ThrowIfCancellationRequested();

            if (link.PredTask is null || link.SuccTask is null)
                continue;

            /*
             * Cross Project Dependency is ignored.
             */
            if (link.IsCrossProject)
                continue;

            var dependency = new MppDependencyModel
            {
                PredecessorUid = link.PredTask.Uid,
                SuccessorUid = link.SuccTask.Uid,
                Type = MapDependencyType(link.LinkType),
                LagMinutes = GetLagMinutes(link)
            };

            result.Add(dependency);
        }

        return result;
    }

    public static List<MppCalendarModel> ParseCalendars(
        Project project, CT ct)
    {
        var result = new List<MppCalendarModel>();

        var defaultCalendar = project.Get(Prj.Calendar);
        var minutesPerDay = project.Get(Prj.MinutesPerDay);

        foreach (var calendar in project.Calendars.Where(c => c.IsBaseCalendar))
        {
            ct.ThrowIfCancellationRequested();

            var model = new MppCalendarModel
            {
                Uid = calendar.Uid,
                Name = calendar.Name ?? $"Calendar-{calendar.Uid}",
                IsDefault = defaultCalendar is not null && defaultCalendar.Uid == calendar.Uid,
                MinutesPerDay = minutesPerDay
            };

            model.WorkingDays = ParseWorkingDays(calendar);
            model.Exceptions = ParseCalendarExceptions(calendar);

            result.Add(model);
        }

        return result;
    }

    public static List<MppCalendarWorkingDayModel> ParseWorkingDays(
        AsposeCalendar calendar)
    {
        var result = new List<MppCalendarWorkingDayModel>();

        foreach (var weekDay in calendar.WeekDays)
        {
            if (!TryMapDayOfWeek(weekDay.DayType, out var dayOfWeek))
                continue;

            var dayModel = new MppCalendarWorkingDayModel
            {
                DayOfWeek = dayOfWeek,
                IsWorking = weekDay.DayWorking
            };

            if (weekDay.DayWorking)
            {
                foreach (var workingTime in weekDay.WorkingTimes)
                {
                    var from = workingTime.From.TimeOfDay;
                    var to = GetWorkingTimeEnd(workingTime.From, workingTime.To);

                    if (from >= to)
                        continue;

                    dayModel.WorkingTimes.Add(
                        new MppCalendarWorkingTimeModel { From = from, To = to });
                }
            }
            dayModel.IsWorking = weekDay.DayWorking && dayModel.WorkingTimes.Count > 0;
            result.Add(dayModel);
        }

        foreach (var day in Enum.GetValues<DayOfWeek>().Where(d => result.All(x => x.DayOfWeek != d)))
        {
            var inherited = calendar.BaseCalendar is null
                ? null
                : ParseWorkingDays(calendar.BaseCalendar).FirstOrDefault(x => x.DayOfWeek == day);

            result.Add(inherited ?? new MppCalendarWorkingDayModel { DayOfWeek = day, IsWorking = false });
        }

        return result;
    }

    public static List<MppCalendarExceptionModel> ParseCalendarExceptions(AsposeCalendar calendar)
    {
        var byDate = calendar.BaseCalendar is null
            ? new Dictionary<DateTime, MppCalendarExceptionModel>()
            : ParseCalendarExceptions(calendar.BaseCalendar).ToDictionary(x => x.Date);

        foreach (var exception in calendar.Exceptions)
        {
            TimeSpan? from = null, to = null;

            var times = exception.DayWorking
                ? exception.WorkingTimes?
                    .Where(t => GetWorkingTimeEnd(t.From, t.To) > t.From.TimeOfDay)
                    .ToList()
                : null;

            if (times is { Count: > 0 })
            {
                // the entity holds one range, so a split shift becomes one continuous range
                from = times.Min(t => t.From.TimeOfDay);
                to = times.Max(t => GetWorkingTimeEnd(t.From, t.To));
            }

            foreach (var date in exception.GetExceptionDates().Select(d => d.Date).Distinct())
            {
                byDate[date] = new MppCalendarExceptionModel   // last one wins, so dates stay unique
                {
                    Date = date,
                    IsWorking = exception.DayWorking,
                    From = from,
                    To = to,
                    Description = exception.Name
                };
            }
        }
        return byDate.Values.OrderBy(x => x.Date).ToList();
    }
    public static long? ToMinutes(
        Duration? duration)
    {
        if (!duration.HasValue)
            return null;

        return (long)Math.Round(duration.Value.TimeSpan.TotalMinutes);
    }

    public static DateTime? NormalizeDate(
        DateTime? date)
    {
        if (!date.HasValue)
            return null;

        if (date.Value == DateTime.MinValue)
            return null;

        return date.Value;
    }

    public static DateTime? NormalizeDate(
        DateTime date)
    {
        if (date == DateTime.MinValue)
            return null;

        return date;
    }

    public static int? GetParentUid(
        AsposeTask task)
    {
        var parent = task.ParentTask;

        if (parent is null)
            return null;

        if (parent.ParentTask is null)
            return null;

        return parent.Uid;
    }

    public static TaskBaseline? GetDefaultBaseline(
        AsposeTask task)
    {
        if (task.Baselines is null || task.Baselines.Count == 0)
            return null;

        return task.Baselines.FirstOrDefault(
            x => x.BaselineNumber == BaselineType.Baseline);
    }

    public static TimeSpan GetWorkingTimeEnd(
        DateTime from,
        DateTime to)
    {
        // If "To" spills into the next day at 00:00, MPP means end-of-day / 24:00.
        // SQL Server time doesn't support 24:00, so cap at 23:59:59.9999999.
        if (to.Date > from.Date && to.TimeOfDay == TimeSpan.Zero)
            return SqlTimeMax;

        return to.TimeOfDay;
    }

    private static readonly TimeSpan SqlTimeMax =
        TimeSpan.FromDays(1) - TimeSpan.FromTicks(1);

    public static bool TryMapDayOfWeek(
        DayType dayType,
        out DayOfWeek dayOfWeek)
    {
        switch (dayType)
        {
            case DayType.Sunday: dayOfWeek = DayOfWeek.Sunday; return true;
            case DayType.Monday: dayOfWeek = DayOfWeek.Monday; return true;
            case DayType.Tuesday: dayOfWeek = DayOfWeek.Tuesday; return true;
            case DayType.Wednesday: dayOfWeek = DayOfWeek.Wednesday; return true;
            case DayType.Thursday: dayOfWeek = DayOfWeek.Thursday; return true;
            case DayType.Friday: dayOfWeek = DayOfWeek.Friday; return true;
            case DayType.Saturday: dayOfWeek = DayOfWeek.Saturday; return true;
            default: dayOfWeek = default; return false;
        }
    }

    public static MppDependencyType MapDependencyType(
        TaskLinkType type)
    {
        return type switch
        {
            TaskLinkType.FinishToStart => MppDependencyType.FinishToStart,
            TaskLinkType.StartToStart => MppDependencyType.StartToStart,
            TaskLinkType.FinishToFinish => MppDependencyType.FinishToFinish,
            TaskLinkType.StartToFinish => MppDependencyType.StartToFinish,
            _ => throw new ArgumentOutOfRangeException(
                nameof(type), type, "نوع وابستگی MPP نامعتبر است.")
        };
    }

    public static long GetLagMinutes(
        TaskLink link)
    {
        // Percent-lag isn't converted here; treated as zero minutes.
        if (link.LagFormat == TimeUnitType.Percent)
            return 0;

        return (long)Math.Round(link.LinkLagTimeSpan.TotalMinutes);
    }
}