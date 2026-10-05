using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectWbses.ImportMPP;

public class MppImportModel
{
    public string? ProjectName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }

    public DateTime? StatusDate { get; set; }

    public List<MppTaskModel> Tasks { get; set; } = [];
    public List<MppDependencyModel> Dependencies { get; set; } = [];
    public List<MppCalendarModel> Calendars { get; set; } = [];
    public List<MppCustomColumnModel> CustomColumns { get; set; } = [];

    // UI Styling configuration
    public UiStylingConfig UiConfig { get; set; } = new();
}

public class MppTaskModel
{
    public int? Id { get; set; }
    public int? Uid { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int OutlineLevel { get; set; }
    public string? OutlineNumber { get; set; }
    public int? ParentUid { get; set; }
    /// <summary>
    /// UID تقویم اختصاصی Task در Microsoft Project
    /// در صورت null بودن، Task از تقویم پیش‌فرض پروژه استفاده می‌کند.
    /// </summary>
    public int? CalendarUid { get; set; }
    public bool IsSummary { get; set; }
    public bool IsMilestone { get; set; }
    public bool IsCritical { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? Finish { get; set; }
    public long? DurationMinutes { get; set; }  
    public decimal? PercentComplete { get; set; }
    public DateTime? BaselineStart { get; set; }
    public DateTime? BaselineFinish { get; set; }
    public long? BaselineDurationMinutes { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualFinish { get; set; }
    public long? RemainingDurationMinutes { get; set; }
    public bool IsManual { get; set; }

    public long? ActualDurationMinutes { get; set; }

    public decimal? PhysicalPercentComplete { get; set; }
    public DateTime? Deadline { get; set; }
    public decimal? Cost { get; set; }
    public string? Note { get; set; }
    public bool IsEstimated { get; set; }
    public List<MppCustomValueModel> CustomValues { get; set; } = [];
}

public class MppCustomColumnModel
{
    public string FieldId { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string? Alias { get; set; }

    public ProjectScheduleColumnDataType DataType { get; set; }
}

public class MppCustomValueModel
{
    public string ColumnCode { get; set; } = string.Empty;
    public string? StringValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateTime? DateTimeValue { get; set; }

    public bool HasValue =>
        StringValue is not null || DecimalValue is not null || DateTimeValue is not null;
}

public class MppDependencyModel
{
    public int? PredecessorUid { get; set; }
    public int? SuccessorUid { get; set; }
    public MppDependencyType Type { get; set; }
    public long LagMinutes { get; set; }
}
public class MppCalendarModel
{
    public int Uid { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDefault { get; set; }

    /// <summary>
    /// تعداد دقایق کاری استاندارد در روز.
    /// مثال: 480 دقیقه = 8 ساعت
    /// </summary>
    public int? MinutesPerDay { get; set; }
    public List<MppCalendarWorkingDayModel> WorkingDays { get; set; } = [];
    public List<MppCalendarExceptionModel> Exceptions { get; set; } = [];
}

public class MppCalendarWorkingDayModel
{
    public DayOfWeek DayOfWeek { get; set; }
    public bool IsWorking { get; set; }
    public List<MppCalendarWorkingTimeModel> WorkingTimes { get; set; } = [];
}

public class MppCalendarWorkingTimeModel
{
    public TimeSpan From { get; set; }
    public TimeSpan To { get; set; }
}

public class MppCalendarExceptionModel
{
    public DateTime Date { get; set; }
    public bool IsWorking { get; set; }
    public TimeSpan? From { get; set; }
    public TimeSpan? To { get; set; }
    public string? Description { get; set; }
}

public class UiStylingConfig
{
    public bool EnableHierarchyStyling { get; set; } = true;
    public bool EnableSummaryStyling { get; set; } = true;
    public bool EnableMilestoneStyling { get; set; } = true;
    public bool EnableCriticalPathStyling { get; set; } = true;

    public Dictionary<int, LevelStyle> LevelStyles { get; set; } = new()
    {
        [1] = new LevelStyle { BackgroundColor = "#E8F0FE", FontSize = 14, FontWeight = "bold" },
        [2] = new LevelStyle { BackgroundColor = "#F0F6FF", FontSize = 12, FontWeight = "bold" },
        [3] = new LevelStyle { BackgroundColor = "#FFFFFF", FontSize = 11, FontWeight = "normal" },
        [4] = new LevelStyle { BackgroundColor = "#FAFAFA", FontSize = 10, FontWeight = "normal" }
    };

    public SummaryStyle SummaryStyle { get; set; } = new()
    {
        FontWeight = "bold",
        FontStyle = "italic",
        BackgroundColor = "#E3F2FD"
    };

    public MilestoneStyle MilestoneStyle { get; set; } = new()
    {
        BackgroundColor = "#FFF3E0",
        FontWeight = "bold"
    };

    public CriticalTaskStyle CriticalTaskStyle { get; set; } = new()
    {
        FontColor = "#D32F2F",
        FontWeight = "bold"
    };
}

public class LevelStyle
{
    public string? BackgroundColor { get; set; }
    public int? FontSize { get; set; }
    public string? FontWeight { get; set; }
}

public class SummaryStyle
{
    public string? FontWeight { get; set; }
    public string? FontStyle { get; set; }
    public string? BackgroundColor { get; set; }
}

public class MilestoneStyle
{
    public string? BackgroundColor { get; set; }
    public string? FontWeight { get; set; }
}

public class CriticalTaskStyle
{
    public string? FontColor { get; set; }
    public string? FontWeight { get; set; }
}

public class TaskStyle
{
    public string? BackgroundColor { get; set; }
    public string? FontColor { get; set; }
    public int? FontSize { get; set; }
    public string? FontWeight { get; set; }
    public string? FontStyle { get; set; }
    public string? TextAlignment { get; set; }
    public bool IsBold { get; set; }
    public bool IsItalic { get; set; }
    public bool IsUnderline { get; set; }
    public string? BorderColor { get; set; }
    public string? BorderStyle { get; set; }
}
