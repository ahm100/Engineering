using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectWbses.Contracts.GetExportMppFile;

public class GetExportMppFileResponse
{
    public string? ProjectName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? StatusDate { get; set; }

    public List<GetExportMppTaskModel> Tasks { get; set; } = [];
    public List<GetExportMppDependencyModel> Dependencies { get; set; } = [];
    public List<GetExportMppCalendarModel> Calendars { get; set; } = [];
    public List<GetExportMppCustomColumnModel> CustomColumns { get; set; } = [];
}

public class GetExportMppTaskModel
{
    public int? Id { get; set; }
    public int? Uid { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? ParentUid { get; set; }

    public int SortOrder { get; set; }
    public int OutlineLevel { get; set; }
    public string? OutlineNumber { get; set; }

    public bool IsSummary { get; set; }

    public DateTime? Start { get; set; }
    public DateTime? Finish { get; set; }
    public long? DurationMinutes { get; set; }

    public decimal PercentComplete { get; set; }

    public DateTime? BaselineStart { get; set; }
    public DateTime? BaselineFinish { get; set; }
    public long? BaselineDurationMinutes { get; set; }

    public DateTime? ActualStart { get; set; }
    public DateTime? ActualFinish { get; set; }
    public long? ActualDurationMinutes { get; set; }

    public bool IsMilestone { get; set; }
    public bool IsCritical { get; set; }
    public bool IsManuallyScheduled { get; set; }

    public int? CalendarUid { get; set; }

    public decimal PhysicalPercentComplete { get; set; }
    public long? RemainingDurationMinutes { get; set; }
    public DateTime? Deadline { get; set; }
    public decimal? Cost { get; set; }
    public string? Note { get; set; }
    public bool IsEstimated { get; set; }

    public List<GetExportMppCustomValueModel> CustomValues { get; set; } = [];
}

public class GetExportMppCalendarModel
{
    public int Uid { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsDefault { get; set; }

    public int MinutesPerDay { get; set; }

    public List<GetExportMppWorkingDayModel> WorkingDays { get; set; } = [];

    public List<GetExportMppCalendarExceptionModel> Exceptions { get; set; } = [];
}

public class GetExportMppDependencyModel
{
    public int? PredecessorUid { get; set; }
    public int? SuccessorUid { get; set; }

    public MppDependencyType Type { get; set; }

    public long LagMinutes { get; set; }
}

public class GetExportMppWorkingDayModel
{
    public DayOfWeek DayOfWeek { get; set; }

    public bool IsWorking { get; set; }

    public List<GetExportMppWorkingTimeModel> WorkingTimes { get; set; } = [];
}

public class GetExportMppWorkingTimeModel
{
    public TimeSpan From { get; set; }

    public TimeSpan To { get; set; }
}

public class GetExportMppCalendarExceptionModel
{
    public DateTime Date { get; set; }

    public bool IsWorking { get; set; }

    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public string? Description { get; set; }
}

public class GetExportMppCustomColumnModel
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public ProjectScheduleColumnDataType DataType { get; set; }
}

public class GetExportMppCustomValueModel
{
    public long ColumnId { get; set; }
    public string? StringValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
}

public static class MppExportSlots
{
    public const int Text = 29;
    public const int Number = 20;
    public const int Date = 10;

    public static bool Exceeds(IEnumerable<ProjectScheduleColumnDataType> types)
    {
        var list = types.ToList();
        return list.Count(t => t == ProjectScheduleColumnDataType.String) > Text
            || list.Count(t => t == ProjectScheduleColumnDataType.Decimal) > Number
            || list.Count(t => t == ProjectScheduleColumnDataType.DateTime) > Date;
    }
}