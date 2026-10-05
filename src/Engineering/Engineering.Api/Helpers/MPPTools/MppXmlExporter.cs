using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Errors;
using Gita.Backend.Shared.Domain.Errors;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Api.Helpers.MppTools;

public static class MppXmlExporter
{
    private static readonly XNamespace Ns =
        "http://schemas.microsoft.com/project";

    public static Result<byte[]> ExportToMppXml<TModel>(
            this TModel data,
            Func<TModel, MppImportModel> mapper)
    {
        try
        {
            if (data is null)
                return Result.Failure<byte[]>(
                    ProjectErrors.MppFileEmptyTask)!;

            ArgumentNullException.ThrowIfNull(mapper);

            var model = mapper(data);

            if (model is null || model.Tasks.Count == 0)
                return Result.Failure<byte[]>(
                    ProjectErrors.MppFileEmptyTask)!;

            var document = BuildDocument(model);

            using var stream = new MemoryStream();

            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\n",
                OmitXmlDeclaration = false
            };

            using (var writer = XmlWriter.Create(stream, settings))
            {
                document.Save(writer);
            }

            var xml = PrettifySections(
                Encoding.UTF8.GetString(stream.ToArray()));

            return Result.Success(
                Encoding.UTF8.GetBytes(xml));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Failure<byte[]>(SharedErrors.UnknownError)!;
        }
    }

    private static XDocument BuildDocument(
        MppImportModel model)
    {
        var defaultCalendar =
            model.Calendars.FirstOrDefault(x => x.IsDefault)
            ?? model.Calendars.FirstOrDefault();

        var minutesPerDay =
            defaultCalendar?.MinutesPerDay > 0
                ? defaultCalendar.MinutesPerDay
                : 480;

        var (orderedTasks, uidToId, uidToDepth) =
            FlattenTasks(model.Tasks);

        // Calculate summary task dates from child tasks
        var summaryDates = CalculateSummaryDates(orderedTasks);

        var customColumns = model.CustomColumns.ToDictionary(c => c.Code);

        // Get unique calendars by UID to avoid duplicates
        var uniqueCalendars = model.Calendars
            .GroupBy(x => x.Uid)
            .Select(g => g.First())
            .ToList();

        var root = new XElement(
            Ns + "Project",

            new XElement(Ns + "SaveVersion", "1"),

            new XElement(
                Ns + "Name",
                model.ProjectName ?? string.Empty),

            new XElement(
                Ns + "Title",
                model.ProjectName ?? string.Empty),

            new XElement(Ns + "ScheduleFromStart", "1"),

            new XElement(
                Ns + "StartDate",
                FormatDateTime(
                    model.StartDate
                    ?? (summaryDates.TryGetValue(0, out var projDates) ? projDates.Start : DateTime.Today))),

            new XElement(
                Ns + "FinishDate",
                FormatDateTime(
                    summaryDates.TryGetValue(0, out var projFinish)
                        ? projFinish.Finish
                        : (model.FinishDate ?? DateTime.Today.AddMonths(1)))),

            new XElement(
                Ns + "CalendarUID",
                defaultCalendar?.Uid.ToString() ?? "1"),

            new XElement(Ns + "DefaultStartTime", "08:00:00"),
            new XElement(Ns + "DefaultFinishTime", "17:00:00"),

            new XElement(
                Ns + "MinutesPerDay",
                minutesPerDay.ToString()),

            new XElement(
                Ns + "MinutesPerWeek",
                (minutesPerDay * 5).ToString()),

            model.StatusDate.HasValue
                 ? new XElement(Ns + "StatusDate", FormatDateTime(model.StatusDate.Value))
                 : null,

            new XElement(
                Ns + "CurrentDate",
                FormatDateTime(DateTime.Now)),

            new XComment(" Views "),
            new XElement(Ns + "Views",
                new XElement(Ns + "View",
                    new XElement(Ns + "Name", "&Gantt Chart"),
                    new XElement(Ns + "IsCustomized", "false")
                )
            ),

            new XComment(" Text Styles "),
            BuildTextStyles(model.UiConfig ?? new UiStylingConfig()),

            BuildExtendedAttributeDefinitions(model.CustomColumns),

            new XComment(" Calendars "),
            BuildCalendars(uniqueCalendars),

            new XComment(" Tasks "),
            BuildTasks(
                orderedTasks,
                uidToId,
                uidToDepth,
                model.Dependencies,
                defaultCalendar!,
                summaryDates,
                customColumns),

            new XComment(
                " Resources / Assignments - not currently tracked "),

            new XElement(Ns + "Resources"),
            new XElement(Ns + "Assignments")
        );

        return new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            root);
    }

    private static Dictionary<int, (DateTime Start, DateTime Finish)> CalculateSummaryDates(
        List<MppTaskModel> orderedTasks)
    {
        var result = new Dictionary<int, (DateTime, DateTime)>();

        // Group tasks by parent
        var byParent = orderedTasks.ToLookup(x => x.ParentUid);

        // Process from bottom up
        foreach (var task in orderedTasks.Where(t => t.IsSummary).OrderByDescending(t => t.OutlineLevel))
        {
            var children = byParent[task.Uid].ToList();
            if (children.Any())
            {
                var start = children
                    .Where(c => c.Start.HasValue)
                    .Min(c => c.Start);

                var finish = children
                    .Where(c => c.Finish.HasValue)
                    .Max(c => c.Finish);

                if (start.HasValue && finish.HasValue)
                {
                    result[task.Uid ?? 0] = (start.Value, finish.Value);
                }
            }
        }

        // Calculate project summary (UID 0)
        var allTasks = orderedTasks.Where(t => t.ParentUid == null || t.ParentUid == 0).ToList();
        if (allTasks.Any())
        {
            var start = allTasks
                .Where(c => c.Start.HasValue)
                .Min(c => c.Start);

            var finish = allTasks
                .Where(c => c.Finish.HasValue)
                .Max(c => c.Finish);

            if (start.HasValue && finish.HasValue)
            {
                result[0] = (start.Value, finish.Value);
            }
        }

        return result;
    }

    private static XElement BuildCalendars(
        IReadOnlyCollection<MppCalendarModel> calendars)
    {
        var calendarsElement =
            new XElement(Ns + "Calendars");

        if (calendars.Count == 0)
        {
            calendarsElement.Add(
                BuildDefaultStandardCalendar());

            return calendarsElement;
        }

        foreach (var calendarModel in calendars)
        {
            var calendarElement =
                new XElement(
                    Ns + "Calendar",

                    new XElement(
                        Ns + "UID",
                        calendarModel.Uid),

                    new XElement(
                        Ns + "Name",
                        calendarModel.Name ??
                        $"Calendar-{calendarModel.Uid}"),

                    new XElement(
                        Ns + "IsBaseCalendar", "1"),

                    BuildWeekDays(calendarModel));

            // Only add exceptions if there are any
            var exceptionsElement = BuildExceptions(calendarModel);
            if (exceptionsElement != null)
                calendarElement.Add(exceptionsElement);

            calendarsElement.Add(calendarElement);
        }

        return calendarsElement;
    }

    private static XElement BuildDefaultStandardCalendar()
    {
        var weekDaysElement =
            new XElement(Ns + "WeekDays");

        for (var dayType = 1; dayType <= 7; dayType++)
        {
            var isWeekend =
                dayType == 1 || dayType == 7;

            var weekDayElement =
                new XElement(
                    Ns + "WeekDay",

                    new XElement(
                        Ns + "DayType",
                        dayType),

                    new XElement(
                        Ns + "DayWorking",
                        isWeekend ? "0" : "1"));

            if (!isWeekend)
            {
                weekDayElement.Add(
                    new XElement(
                        Ns + "WorkingTimes",

                        new XElement(
                            Ns + "WorkingTime",

                            new XElement(
                                Ns + "FromTime",
                                "08:00:00"),

                            new XElement(
                                Ns + "ToTime",
                                "17:00:00"))));
            }

            weekDaysElement.Add(weekDayElement);
        }

        return new XElement(
            Ns + "Calendar",

            new XElement(Ns + "UID", 1),
            new XElement(Ns + "Name", "Standard"),
            new XElement(Ns + "IsBaseCalendar", "1"),
            weekDaysElement);
    }

    private static XElement BuildWeekDays(
        MppCalendarModel calendarModel)
    {
        var weekDaysElement =
            new XElement(Ns + "WeekDays");

        foreach (var dayModel in calendarModel.WorkingDays)
        {
            if (!TryMapDayType(
                    dayModel.DayOfWeek,
                    out var dayType))
                continue;

            var weekDayElement =
                new XElement(
                    Ns + "WeekDay",

                    new XElement(
                        Ns + "DayType",
                        dayType),

                    new XElement(
                        Ns + "DayWorking",
                        dayModel.IsWorking ? "1" : "0"));

            if (dayModel.IsWorking && dayModel.WorkingTimes.Any())
            {
                var validTimes =
                    dayModel.WorkingTimes
                        .Where(x => x.To > x.From)
                        .ToList();

                var workingTimesElement =
                    new XElement(Ns + "WorkingTimes");

                if (validTimes.Count == 0)
                {
                    workingTimesElement.Add(
                        new XElement(
                            Ns + "WorkingTime",

                            new XElement(
                                Ns + "FromTime",
                                "08:00:00"),

                            new XElement(
                                Ns + "ToTime",
                                "17:00:00")));
                }
                else
                {
                    foreach (var time in validTimes)
                    {
                        workingTimesElement.Add(
                            new XElement(
                                Ns + "WorkingTime",

                                new XElement(
                                    Ns + "FromTime",
                                    FormatTime(time.From)),

                                new XElement(
                                    Ns + "ToTime",
                                    FormatTime(time.To))));
                    }
                }

                weekDayElement.Add(
                    workingTimesElement);
            }

            weekDaysElement.Add(weekDayElement);
        }

        return weekDaysElement;
    }

    private static XElement? BuildExceptions(
        MppCalendarModel calendarModel)
    {
        if (calendarModel.Exceptions.Count == 0)
            return null;

        var exceptionsElement =
            new XElement(Ns + "Exceptions");

        foreach (var exceptionModel in calendarModel.Exceptions)
        {
            var fromDate =
                exceptionModel.Date.Date;

            var toDate =
                exceptionModel.Date.Date
                    .AddHours(23)
                    .AddMinutes(59);

            var exceptionElement =
                new XElement(
                    Ns + "Exception",

                    new XElement(
                        Ns + "TimePeriod",

                        new XElement(
                            Ns + "FromDate",
                            FormatDateTime(fromDate)),

                        new XElement(
                            Ns + "ToDate",
                            FormatDateTime(toDate))),

                    new XElement(
                        Ns + "Name",
                        exceptionModel.Description ??
                        string.Empty),

                    new XElement(
                        Ns + "DayWorking",
                        exceptionModel.IsWorking
                            ? "1"
                            : "0"));

            if (exceptionModel.IsWorking &&
                exceptionModel.From.HasValue &&
                exceptionModel.To.HasValue)
            {
                exceptionElement.Add(
                    new XElement(
                        Ns + "WorkingTimes",

                        new XElement(
                            Ns + "WorkingTime",

                            new XElement(
                                Ns + "FromTime",
                                FormatTime(
                                    exceptionModel.From.Value)),

                            new XElement(
                                Ns + "ToTime",
                                FormatTime(
                                    exceptionModel.To.Value)))));
            }

            exceptionsElement.Add(exceptionElement);
        }

        return exceptionsElement;
    }

    private static XElement BuildTasks(
        IReadOnlyCollection<MppTaskModel> orderedTasks,
        IReadOnlyDictionary<int, int> uidToId,
        IReadOnlyDictionary<int, int> uidToDepth,
        IReadOnlyCollection<MppDependencyModel> dependencies,
        MppCalendarModel defaultCalendar,
        Dictionary<int, (DateTime Start, DateTime Finish)> summaryDates,
        IReadOnlyDictionary<string, MppCustomColumnModel> customColumns)
    {
        var tasksElement =
            new XElement(Ns + "Tasks");

        // Project Summary Task with proper dates
        var projectStart = summaryDates.TryGetValue(0, out var projDates) ? projDates.Start : DateTime.Today;
        var projectFinish = summaryDates.TryGetValue(0, out var projFinish) ? projFinish.Finish : DateTime.Today.AddMonths(1);

        tasksElement.Add(
            new XElement(
                Ns + "Task",

                new XElement(Ns + "UID", 0),
                new XElement(Ns + "ID", 0),
                new XElement(
                    Ns + "Name",
                    "Project Summary"),
                new XElement(
                    Ns + "OutlineLevel",
                    0),
                new XElement(
                    Ns + "Summary",
                    "1"),
                new XElement(
                    Ns + "ConstraintType",
                    "0"),
                new XElement(
                    Ns + "ConstraintDate",
                    FormatDateTime(projectStart)),
                new XElement(
                    Ns + "Start",
                    FormatDateTime(projectStart)),
                new XElement(
                    Ns + "Finish",
                    FormatDateTime(projectFinish))));

        var dependenciesBySuccessor =
            dependencies
                .GroupBy(x => x.SuccessorUid)
                .ToDictionary(
                    x => x.Key,
                    x => x.ToList());

        foreach (var taskModel in orderedTasks)
        {
            var uid = taskModel.Uid ?? 0;
            var taskElement = new XElement(Ns + "Task");

            void Add(string name, object? value) =>
                taskElement.Add(new XElement(Ns + name, value));

            // summaries use the dates calculated from their children
            var start = taskModel.Start;
            var finish = taskModel.Finish;
            if (taskModel.IsSummary && summaryDates.TryGetValue(uid, out var summaryDate))
            {
                start = summaryDate.Start;
                finish = summaryDate.Finish;
            }

            Add("UID", uid);
            Add("ID", uidToId[uid]);
            Add("Name", taskModel.Name ?? string.Empty);
            Add("Active", "1");
            Add("Manual", taskModel.IsManual ? "1" : "0");
            Add("OutlineLevel", uidToDepth[uid]);

            if (start.HasValue) Add("Start", FormatDateTime(start.Value));
            if (finish.HasValue) Add("Finish", FormatDateTime(finish.Value));

            if (taskModel.DurationMinutes.HasValue)
            {
                Add("Duration",
                    taskModel.IsMilestone || taskModel.DurationMinutes.Value == 0
                        ? "PT0H0M0S"
                        : FormatDuration(taskModel.DurationMinutes.Value));
                Add("DurationFormat", "7");
            }

            Add("Estimated", taskModel.IsEstimated ? "1" : "0");
            Add("Milestone", taskModel.IsMilestone ? "1" : "0");
            Add("Summary", taskModel.IsSummary ? "1" : "0");
            Add("Critical", taskModel.IsCritical ? "1" : "0");
            Add("PercentComplete", ((int)(taskModel.PercentComplete ?? 0)).ToString());

            if (taskModel.ActualStart.HasValue)
                Add("ActualStart", FormatDateTime(taskModel.ActualStart.Value));
            if (taskModel.ActualFinish.HasValue)
                Add("ActualFinish", FormatDateTime(taskModel.ActualFinish.Value));
            if (taskModel.ActualDurationMinutes.HasValue)
                Add("ActualDuration", FormatDuration(taskModel.ActualDurationMinutes.Value));
            if (taskModel.RemainingDurationMinutes.HasValue)
                Add("RemainingDuration", FormatDuration(taskModel.RemainingDurationMinutes.Value));

            Add("ConstraintType", taskModel.IsMilestone ? "3" : "0");
            Add("CalendarUID", taskModel.CalendarUid ?? -1);          // -1 = no task calendar (what MS Project writes)
            Add("ConstraintDate", FormatDateTime(taskModel.Start ?? projectStart));

            if (taskModel.Deadline.HasValue)
                Add("Deadline", FormatDateTime(taskModel.Deadline.Value));

            var note = CleanText(taskModel.Note);
            if (!string.IsNullOrEmpty(note))
                Add("Notes", note);

            Add("PhysicalPercentComplete", ((int)(taskModel.PhysicalPercentComplete ?? 0)).ToString());

            // predecessor links come after PhysicalPercentComplete
            if (dependenciesBySuccessor.TryGetValue(taskModel.Uid, out var predecessorLinks))
            {
                foreach (var dependency in predecessorLinks)
                {
                    taskElement.Add(
                        new XElement(
                            Ns + "PredecessorLink",
                            new XElement(Ns + "PredecessorUID", dependency.PredecessorUid),
                            new XElement(Ns + "Type", MapLinkType(dependency.Type)),
                            new XElement(Ns + "CrossProject", "0"),
                            new XElement(Ns + "LinkLag", dependency.LagMinutes * 10),
                            new XElement(Ns + "LagFormat", "3")));
                }
            }

            taskElement.Add(
                new XElement(
                    Ns + "ExtendedAttribute",
                    new XElement(Ns + "FieldID", "188743731"),
                    new XElement(Ns + "Value", uid.ToString())));

            foreach (var value in taskModel.CustomValues)
            {
                if (!value.HasValue || !customColumns.TryGetValue(value.ColumnCode, out var column)) continue;

                var text = column.DataType switch
                {
                    ProjectScheduleColumnDataType.Decimal => value.DecimalValue?.ToString(CultureInfo.InvariantCulture),
                    ProjectScheduleColumnDataType.DateTime => value.DateTimeValue is { } d ? FormatDateTime(d) : null,
                    _ => CleanText(value.StringValue)
                };
                if (string.IsNullOrEmpty(text)) continue;

                taskElement.Add(new XElement(Ns + "ExtendedAttribute",
                    new XElement(Ns + "FieldID", column.FieldId),
                    new XElement(Ns + "Value", text)));
            }

            // baseline is the last element of a task
            if (taskModel.BaselineStart.HasValue ||
                taskModel.BaselineFinish.HasValue ||
                taskModel.BaselineDurationMinutes.HasValue)
            {
                var baselineElement = new XElement(Ns + "Baseline", new XElement(Ns + "Number", "0"));

                if (taskModel.BaselineStart.HasValue)
                    baselineElement.Add(new XElement(Ns + "Start", FormatDateTime(taskModel.BaselineStart.Value)));

                if (taskModel.BaselineFinish.HasValue)
                    baselineElement.Add(new XElement(Ns + "Finish", FormatDateTime(taskModel.BaselineFinish.Value)));

                if (taskModel.BaselineDurationMinutes.HasValue)
                {
                    baselineElement.Add(new XElement(Ns + "Duration", FormatDuration(taskModel.BaselineDurationMinutes.Value)));
                    baselineElement.Add(new XElement(Ns + "DurationFormat", "7"));
                }

                taskElement.Add(baselineElement);
            }

            tasksElement.Add(taskElement);
        }

        return tasksElement;
    }

    private static string PrettifySections(
        string xml)
    {
        xml = Regex.Replace(
            xml,
            @"(</Calendar>\n)(\s*<Calendar>)",
            "$1\n$2");

        xml = Regex.Replace(
            xml,
            @"(</Task>\n)(\s*<Task>)",
            "$1\n$2");

        return xml;
    }

    private static (
        List<MppTaskModel> Ordered,
        Dictionary<int, int> UidToId,
        Dictionary<int, int> UidToDepth)
        FlattenTasks(
            IReadOnlyCollection<MppTaskModel> tasks)
    {
        var byParent =
            tasks.ToLookup(x => x.ParentUid);

        var ordered =
            new List<MppTaskModel>();

        var uidToDepth =
            new Dictionary<int, int>();

        void Visit(
            int? parentUid,
            int depth)
        {
            var children =
                byParent[parentUid]
                    .OrderBy(x => x.SortOrder);

            foreach (var child in children)
            {
                ordered.Add(child);

                uidToDepth[child.Uid ?? 0] =
                    depth;

                Visit(
                    child.Uid,
                    depth + 1);
            }
        }

        Visit(null, 1);

        var uidToId =
            new Dictionary<int, int>();

        for (var i = 0;
             i < ordered.Count;
             i++)
        {
            uidToId[ordered[i].Uid ?? 0] =
                i + 1;
        }

        return (
            ordered,
            uidToId,
            uidToDepth);
    }

    private static string FormatDateTime(
        DateTime value) =>
        value.ToString(
            "yyyy-MM-ddTHH:mm:ss",
            CultureInfo.InvariantCulture);

    private static string FormatTime(
        TimeSpan value) =>
        DateTime.Today
            .Add(value)
            .ToString(
                "HH:mm:ss",
                CultureInfo.InvariantCulture);

    private static string FormatDuration(
        long totalMinutes)
    {
        var hours =
            totalMinutes / 60;

        var minutes =
            totalMinutes % 60;

        return $"PT{hours}H{minutes}M0S";
    }

    private static string? CleanText(string? value)
    {
        return value is null
        ? null
        : new string(value.Where(c => c >= ' ' || c is '\t' or '\n' or '\r').ToArray());
    }

    private static bool TryMapDayType(
        DayOfWeek dayOfWeek,
        out int dayType)
    {
        dayType = dayOfWeek switch
        {
            DayOfWeek.Sunday => 1,
            DayOfWeek.Monday => 2,
            DayOfWeek.Tuesday => 3,
            DayOfWeek.Wednesday => 4,
            DayOfWeek.Thursday => 5,
            DayOfWeek.Friday => 6,
            DayOfWeek.Saturday => 7,
            _ => 0
        };

        return dayType != 0;
    }

    private static int MapLinkType(
        MppDependencyType type)
    {
        return type switch
        {
            MppDependencyType.FinishToFinish => 0,
            MppDependencyType.FinishToStart => 1,
            MppDependencyType.StartToFinish => 2,
            MppDependencyType.StartToStart => 3,

            _ => throw new ArgumentOutOfRangeException(
                nameof(type),
                type,
                "نوع وابستگی نامعتبر است.")
        };
    }
    private static XElement BuildTextStyles(UiStylingConfig config)
    {
        var styles = new XElement(Ns + "TextStyles");
        styles.Add(BuildTextStyle(0, "Arial", 11, false, false, "#000000", "#FFFFFF"));
        styles.Add(BuildTextStyle(1, "Arial", 12, true, true, "#000000", config.SummaryStyle?.BackgroundColor ?? "#E3F2FD"));
        styles.Add(BuildTextStyle(2, "Arial", 11, true, false, "#000000", config.MilestoneStyle?.BackgroundColor ?? "#FFF3E0"));
        styles.Add(BuildTextStyle(3, "Arial", 11, true, false, config.CriticalTaskStyle?.FontColor ?? "#D32F2F", "#FFFFFF"));
        return styles;
    }

    private static XElement BuildTextStyle(int item, string font, int size, bool bold, bool italic, string color, string background)
    {
        return new XElement(Ns + "TextStyle",
            new XAttribute("Item", item),
            new XAttribute("Font", font),
            new XAttribute("Size", size),
            new XAttribute("Bold", bold ? "1" : "0"),
            new XAttribute("Italic", italic ? "1" : "0"),
            new XAttribute("Underline", "0"),
            new XAttribute("Color", OLEColorFromHex(color)),
            new XAttribute("Background", OLEColorFromHex(background)),
            new XAttribute("BackgroundPattern", "1"));
    }

    private static XElement? BuildExtendedAttributeDefinitions(
        IReadOnlyCollection<MppCustomColumnModel> columns)
    {
        if (columns.Count == 0) return null;   // XElement ignores null content

        return new XElement(Ns + "ExtendedAttributes",
            columns.Select(c => new XElement(Ns + "ExtendedAttribute",
                new XElement(Ns + "FieldID", c.FieldId),
                new XElement(Ns + "FieldName", c.Code),
                new XElement(Ns + "Alias", c.Alias ?? c.Code))));
    }

    private static int OLEColorFromHex(string hex)
    {
        if (string.IsNullOrEmpty(hex)) return 0;
        hex = hex.TrimStart('#');
        if (hex.Length != 6) return 0;
        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);
        return (b << 16) | (g << 8) | r;
    }
}