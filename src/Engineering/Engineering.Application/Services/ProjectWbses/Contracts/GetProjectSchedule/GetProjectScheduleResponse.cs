using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectWbses.Contracts.GetProjectSchedule;

public class GetProjectScheduleResponse
{
    public long ImportId { get; set; }
    public long ProjectId { get; set; }
    public Guid? FileId { get; set; }
    public string? FileName { get; set; } = string.Empty;
    public long ImportedBy { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? ScheduleStartDate { get; set; }
    public decimal PlanPercent { get; set; }
    public decimal ActualPercent { get; set; }

    public List<GetProjectScheduleTaskModel> Tasks { get; set; } = [];
    public List<GetProjectScheduleDependencyModel> Dependencies { get; set; } = [];
    public List<GetProjectScheduleCalendarModel> Calendars { get; set; } = [];
    public List<GetProjectScheduleColumnModel> Columns { get; set; } = [];
}


public class GetProjectScheduleColumnModel
{
    public long Id { get; set; }
    public ProjectScheduleColumnType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public string TitleFa { get; set; } = string.Empty;
    public string? TitleEn { get; set; }
    public int SortOrder { get; set; }
    public ProjectScheduleColumnDataType DataType { get; set; }
    public string DataTypeDescription => DataType.GetEnumDescription();
    public bool IsSystem { get; set; }
}

public class GetProjectScheduleWbsModel
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int OutlineLevel { get; set; }

    public decimal PlanPercent { get; set; }
    public decimal ActualPercent { get; set; }

    public List<long> TaskIds { get; set; } = [];
}


public class GetProjectScheduleTaskModel
{
    public long Id { get; set; }
    public long? ParentTaskId { get; set; }
    public bool IsSummary { get; set; }
    public string Title { get; set; } = string.Empty;

    public bool HasChildren { get; set; }
    public int CountChildren { get; set; }
    public bool? IsEstimated { get; set; }
    public int? MppId { get; set; }
    public int? MppUid { get; set; }
    public int SortOrder { get; set; }
    public int OutlineLevel { get; set; }
    public string? OutlineNumber { get; set; }

    public DateTime? PlannedStart { get; set; }
    public DateTime? PlannedFinish { get; set; }
    public long? PlannedDurationMinutes { get; set; }

    public decimal PercentComplete { get; set; }
    public decimal PhysicalPercentComplete { get; set; }
    public decimal SchedulePercentComplete { get; set; }
    public decimal PlanPercent { get; set; }
    public decimal ActualPercent { get; set; }
    public DateTime? ScheduleStartDate { get; set; }
    public DateTime? BaselineStart { get; set; }
    public DateTime? BaselineFinish { get; set; }
    public long? BaselineDurationMinutes { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualFinish { get; set; }
    public long? ActualDurationMinutes { get; set; }

    public bool IsMilestone { get; set; }
    public bool IsCritical { get; set; }
    public bool IsManuallyScheduled { get; set; }
    public decimal Weight { get; set; }

    public List<GetProjectScheduleTaskValueModel> CustomValues { get; set; } = [];
    public List<GetProjectScheduleTaskModel> Children { get; set; } = [];
}

public class GetProjectScheduleDependencyModel
{
    public long Id { get; set; }
    public long PredecessorTaskId { get; set; }
    public long SuccessorTaskId { get; set; }
    public ProjectScheduleDependencyType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public long LagMinutes { get; set; }
}


public class GetProjectScheduleCalendarModel
{
    public long Id { get; set; }

    public int? MppUid { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool IsDefault { get; set; }

    public int MinutesPerDay { get; set; }

    public List<GetProjectScheduleWorkingDayModel> WorkingDays { get; set; } = [];

    public List<GetProjectScheduleCalendarExceptionModel> Exceptions { get; set; } = [];
}


public class GetProjectScheduleWorkingDayModel
{
    public long Id { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public bool IsWorking { get; set; }

    public List<GetProjectScheduleWorkingTimeModel> WorkingTimes { get; set; } = [];
}


public class GetProjectScheduleWorkingTimeModel
{
    public long Id { get; set; }

    public TimeSpan From { get; set; }

    public TimeSpan To { get; set; }
}


public class GetProjectScheduleCalendarExceptionModel
{
    public long Id { get; set; }

    public DateTime Date { get; set; }

    public bool IsWorking { get; set; }

    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public string? Description { get; set; }
}

public class GetProjectScheduleTaskValueModel
{
    public long ColumnId { get; set; }
    public string? Value { get; set; }
    public decimal? NumberValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
}