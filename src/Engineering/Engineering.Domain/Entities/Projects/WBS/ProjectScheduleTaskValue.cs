namespace Engineering.Domain.Entities.Projects.WBS;

[Description(WbsCmts.ProjectScheduleTaskValue)]
public class ProjectScheduleTaskValue : ActivateEntity<ProjectScheduleTaskValue, long>
{
    [Description(WbsCmts.ProjectScheduleTask)]
    public long ProjectScheduleTaskId { get; private set; }
    public ProjectScheduleTask ProjectScheduleTask { get; private set; }

    [Description(WbsCmts.ProjectScheduleColumn)]
    public long ProjectScheduleColumnId { get; private set; }
    public ProjectScheduleColumn ProjectScheduleColumn { get; private set; }

    [Description(GlobalCmts.Value)]
    public string? Value { get; private set; }

    [Description(WbsCmts.NumberValue)]
    public decimal? NumberValue { get; private set; }

    [Description(WbsCmts.DateTimeValue)]
    public DateTime? DateTimeValue { get; private set; }

    public ProjectScheduleTaskValue(
        ProjectScheduleTask task,
        ProjectScheduleColumn column) : this()
    {
        ProjectScheduleTask = Guard.Against.Null(task);
        ProjectScheduleColumn = Guard.Against.Null(column);

        SetActive();
    }

    public void SetText(string? value)
    {
        Value = value;
        NumberValue = null;
        DateTimeValue = null;
    }

    public void SetNumber(decimal? value)
    {
        NumberValue = value;
        Value = null;
        DateTimeValue = null;
    }

    public void SetDateTime(DateTime? value)
    {
        DateTimeValue = value;
        Value = null;
        NumberValue = null;
    }

#pragma warning disable CS8618
    private ProjectScheduleTaskValue() { }
#pragma warning restore CS8618
}