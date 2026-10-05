using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Domain.Entities.Projects.WBS;

[Description(WbsCmts.ProjectScheduleTaskOperation)]
public class ProjectScheduleTaskOperation : ActivateEntity<ProjectScheduleTaskOperation, long>
{
    [Description(WbsCmts.ProjectScheduleTask)]
    public long ProjectScheduleTaskId { get; private set; }
    public ProjectScheduleTask ProjectScheduleTask { get; private set; }

    [Description(GlobalCmts.ProjectOperation)]
    public long ProjectOperationId { get; private set; }
    public ProjectOperation ProjectOperation { get; private set; }

    public ProjectScheduleTaskOperation(
        ProjectScheduleTask projectScheduleTask,
        ProjectOperation projectOperation,
        bool isActive) : this()
    {
        SetProjectOperation(projectOperation);
        SetProjectScheduleTask(projectScheduleTask);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public void Update(
        ProjectScheduleTask projectScheduleTask,
        ProjectOperation projectOperation,
        bool? isActive)
    {
        SetProjectScheduleTask(projectScheduleTask);
        SetProjectOperation(projectOperation);

        if (isActive is not null && isActive.Value)
            SetActive();
        else if (isActive is not null && !isActive.Value)
            SetDeactivate();
    }

    public void SetProjectOperation(ProjectOperation value)
    {
        ProjectOperation = Guard.Against.Null(value, nameof(value));
        ProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProjectScheduleTask(ProjectScheduleTask value)
    {
        ProjectScheduleTask = Guard.Against.Null(value, nameof(value));
        ProjectScheduleTaskId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectScheduleTaskOperation() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}