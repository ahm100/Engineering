using Engineering.Domain.Entities.Tasks.Enums;

namespace Engineering.Domain.Entities.Tasks;

[Description("User Task")]
public class UserTask : ActivateEntity<UserTask, long>
{
    [Description(TasksCmts.TaskName)]
    public string Title { get; private set; } = string.Empty;
    [Description(TasksCmts.Description)]
    public string? Description { get; private set; }
    public long TaskGroupId { get; private set; }
    public TaskGroup TaskGroup { get; private set; } = null!;
    [Description(TasksCmts.Status)]
    public TaskStatusEnum Status { get; private set; } = TaskStatusEnum.ToDo;

    public long? OwnerUserId { get; private set; }

    public UserTask(
        string title,
        string? description,
        long taskGroupId,
        TaskStatusEnum status,
        long? ownerUserId = null) : this()  
    {
        SetTitle(title);
        SetDescription(description);
        SetTaskGroupId(taskGroupId);
        SetStatus(status);
        SetOwnerUserId(ownerUserId); 
    }

    public void Update(string title, string? description, long taskGroupId)
    {
        SetTitle(title);
        if (description is not null)
            SetDescription(description);
        SetTaskGroupId(taskGroupId);
    }

    public void Update(string title, string? description, long taskGroupId, TaskStatusEnum status)
    {
        Update(title, description, taskGroupId);
        SetStatus(status);
    }

    public void SetTitle(string value)
        => Title = Guard.Against.NullOrWhiteSpace(value, nameof(value));

    public void SetDescription(string? value)
        => Description = value;

    public void SetTaskGroupId(long value)
        => TaskGroupId = value;

    public void SetStatus(TaskStatusEnum value)
        => Status = value;

    public void SetOwnerUserId(long? value)
        => OwnerUserId = value;

    private UserTask() { }
}