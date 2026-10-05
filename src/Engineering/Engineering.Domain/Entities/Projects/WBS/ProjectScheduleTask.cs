using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Domain.Entities.Projects.WBS;

[Description(WbsCmts.ProjectScheduleTask)]
public class ProjectScheduleTask : ActivateEntity<ProjectScheduleTask, long>
{
    [Description(GlobalCmts.Title)]
    public string Title { get; private set; } = string.Empty;

    [Description(WbsCmts.MppUid)]
    public int? MppUid { get; private set; }

    [Description(WbsCmts.MppId)]
    public int? MppId { get; private set; }

    [Description(WbsCmts.SortOrder)]
    public int SortOrder { get; private set; }

    [Description(WbsCmts.OutlineLevel)]
    public int OutlineLevel { get; private set; }

    [Description(WbsCmts.OutlineNumber)]
    public string? OutlineNumber { get; private set; }

    [Description(WbsCmts.PlannedStart)]
    public DateTime? PlannedStart { get; private set; }

    [Description(WbsCmts.PlannedFinish)]
    public DateTime? PlannedFinish { get; private set; }

    [Description(WbsCmts.PlannedDurationMinutes)]
    public long? PlannedDurationMinutes { get; private set; }

    [Description(WbsCmts.PercentComplete)]
    public decimal PercentComplete { get; private set; }

    [Description(WbsCmts.PhysicalPercentComplete)]
    public decimal PhysicalPercentComplete { get; private set; }

    [Description(WbsCmts.BaselineStart)]
    public DateTime? BaselineStart { get; private set; }

    [Description(WbsCmts.BaselineFinish)]
    public DateTime? BaselineFinish { get; private set; }

    [Description(WbsCmts.BaselineDurationMinutes)]
    public long? BaselineDurationMinutes { get; private set; }

    [Description(WbsCmts.ActualStart)]
    public DateTime? ActualStart { get; private set; }

    [Description(WbsCmts.ActualFinish)]
    public DateTime? ActualFinish { get; private set; }

    [Description(WbsCmts.ActualDurationMinutes)]
    public long? ActualDurationMinutes { get; private set; }

    [Description(WbsCmts.IsMilestone)]
    public bool IsMilestone { get; private set; }

    [Description(WbsCmts.IsCritical)]
    public bool IsCritical { get; private set; }

    [Description(WbsCmts.IsManuallyScheduled)]
    public bool IsManuallyScheduled { get; private set; }

    [Description(WbsCmts.IsEstimated)]
    public bool IsEstimated { get; private set; }

    [Description(WbsCmts.ProjectScheduleImport)]
    public long ProjectScheduleImportId { get; private set; }
    public ProjectScheduleImport? ProjectScheduleImport { get; private set; }

    [Description(WbsCmts.ProjectWbs)]
    public long? ProjectWbsId { get; private set; }
    public ProjectWbs? ProjectWbs { get; private set; }

    [Description(WbsCmts.ProjectScheduleTask)]
    public long? ParentId { get; private set; }
    public ProjectScheduleTask? Parent { get; private set; }

    [Description(WbsCmts.IsSummary)]
    public bool? IsSummary { get; private set; }

    [Description(WbsCmts.Deadline)]
    public DateTime? Deadline { get; private set; }

    [Description(WbsCmts.Cost)]
    public decimal? Cost { get; private set; }

    [Description(WbsCmts.Note)]
    public string? Note { get; private set; }

    [Description(WbsCmts.RemainingDurationMinutes)]
    public long? RemainingDurationMinutes { get; private set; }

    [Description(WbsCmts.Calendar)]
    public long? CalendarId { get; private set; }
    public ProjectCalendar? Calendar { get; set; }

    [Description(GlobalCmts.Weight)]
    public decimal Weight { get; private set; }

    public ProjectScheduleTask(
        ProjectScheduleImport import,
        ProjectWbs? projectWbs,
        string title,
        int? mppUid,
        int? mppId,
        int sortOrder,
        int outlineLevel,
        string? outlineNumber,
        DateTime? plannedStart,
        DateTime? plannedFinish,
        long? plannedDurationMinutes,
        decimal percentComplete,
        DateTime? baselineStart,
        DateTime? baselineFinish,
        long? baselineDurationMinutes,
        DateTime? actualStart,
        DateTime? actualFinish,
        long? actualDurationMinutes,
        bool isMilestone,
        bool isCritical,
        bool isSummary,
        bool isManuallyScheduled) : this()
    {
        ProjectScheduleImport = import;
        ProjectScheduleImportId = import.Id;

        ProjectWbs = projectWbs;
        ProjectWbsId = projectWbs?.Id;

        Title = Guard.Against.NullOrEmpty(title);

        MppUid = mppUid;
        MppId = mppId;

        SortOrder = sortOrder;
        OutlineLevel = outlineLevel;
        OutlineNumber = outlineNumber;

        PlannedStart = plannedStart;
        PlannedFinish = plannedFinish;
        PlannedDurationMinutes = plannedDurationMinutes;

        PercentComplete = percentComplete;

        BaselineStart = baselineStart;
        BaselineFinish = baselineFinish;
        BaselineDurationMinutes = baselineDurationMinutes;

        ActualStart = actualStart;
        ActualFinish = actualFinish;
        ActualDurationMinutes = actualDurationMinutes;

        IsMilestone = isMilestone;
        IsSummary = isSummary;
        IsCritical = isCritical;

        SetSchedulingMode(isManuallyScheduled);

        SetActive();
    }

    public void Update(
        long? plannedDurationMinutes)
    {
        SetPlannedDurationMinutes(plannedDurationMinutes);
    }

    public void SetIsMilestone(bool value)
    {
        if (value)
        {
            PlannedDurationMinutes = 0;
            RemainingDurationMinutes = 0;
            IsEstimated = false;

            if (PlannedStart.HasValue) 
                PlannedFinish = PlannedStart;
            else if 
                (PlannedFinish.HasValue) PlannedStart = PlannedFinish;
        }

        IsMilestone = value;
    }

    public void SetPlannedDuration(long durationMinutes, bool isEstimated)
    {
        if (IsMilestone && durationMinutes != 0)
            IsMilestone = false;

        PlannedDurationMinutes = durationMinutes;
        IsEstimated = isEstimated;
    }

    public void SetPlannedDurationMinutes(long? value)
    {
        PlannedDurationMinutes = value;
    }

    public void SetTitle(string value)
    {
        Title = value;
    }

    public void SetSchedulingMode(bool isManual)
    {
        IsManuallyScheduled = isManual;
    }

    public void SetSchedule(DateTime start, DateTime finish, long durationMinutes, bool isEstimated)
    {
        PlannedStart = start;
        PlannedFinish = finish;
        PlannedDurationMinutes = durationMinutes;
        IsEstimated = isEstimated;
    }

    public void SetActualStart(DateTime? value)
    {
        ActualStart = value;
    }

    public void SetActualFinish(DateTime? value)
    {
        ActualFinish = value;
        if (value.HasValue)
        {
            PercentComplete = 100;
            PhysicalPercentComplete = 100;
        }
    }

    public void SetPlannedStart(DateTime? value)
    {
        PlannedStart = value;
    }

    public void SetPlannedFinish(DateTime? value)
    {
        PlannedFinish = value;
    }

    public void SetPercentComplete(decimal value)
    {
        PercentComplete = Math.Clamp(value, 0, 100);
        if (PercentComplete < 100)
            ActualFinish = null;
    }

    public void SetPhysicalPercentComplete(decimal value)
    {
        PhysicalPercentComplete = value;
    }

    public void SetActualDurationMinutes(long value)
    {
        ActualDurationMinutes = value;
    }

    public void SetParentTask(ProjectScheduleTask? value)
    {
        Parent = value;
        ParentId = value?.Id;
    }

    public void SetIsSummary(bool value)
    {
        IsSummary = value;
    }

    public void SetOutline(int outlineLevel, string? outlineNumber, int sortOrder)
    {
        OutlineLevel = outlineLevel;
        OutlineNumber = outlineNumber;
        SortOrder = sortOrder;
    }

    public void SetRemainingDuration(long? value)
    {
        RemainingDurationMinutes = value;
    }

    public void SetDeadline(DateTime? value)
    {
        Deadline = value;
    }

    public void SetCost(decimal? value)
    {
        Cost = value;
    }

    public void SetNote(string? value)
    {
        Note = value;
    }

    public void SetEstimated(bool value)
    {
        IsEstimated = value;
    }

    public void SetSortOrder(int value)
    {
        SortOrder = value;
    }

    public void SetCalendar(ProjectCalendar? value)
    {
        Calendar = value;
        CalendarId = value?.Id;
    }

    public void SetIsCritical(bool value)
    {
        IsCritical = value;
    }

    public void SetWeight(decimal value)
    {
        Weight = Guard.Against.Negative(value);
    }

    private readonly List<ProjectScheduleTask> _childs;
    public IReadOnlyList<ProjectScheduleTask> Childs => _childs;

    private readonly List<ProjectScheduleTaskDependency> _successorDependencies;
    public IReadOnlyList<ProjectScheduleTaskDependency> SuccessorDependencies => _successorDependencies;

    private readonly List<ProjectScheduleTaskDependency> _predecessorDependencies;
    public IReadOnlyList<ProjectScheduleTaskDependency> PredecessorDependencies => _predecessorDependencies;

    private readonly List<ProjectScheduleTaskOperation> _projectScheduleTaskOperations;
    public IReadOnlyList<ProjectScheduleTaskOperation> ProjectScheduleTaskOperations => _projectScheduleTaskOperations;

    private readonly List<ProjectScheduleTaskValue> _projectScheduleTaskValues;
    public IReadOnlyList<ProjectScheduleTaskValue> ProjectScheduleTaskValues => _projectScheduleTaskValues;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectScheduleTask()
    {
        _childs = [];
        _projectScheduleTaskOperations = [];
        _successorDependencies = [];
        _predecessorDependencies = [];
        _projectScheduleTaskValues = [];
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}