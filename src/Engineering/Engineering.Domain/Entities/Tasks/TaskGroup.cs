using Engineering.Domain.Entities.Tasks;

namespace Engineering.Domain.Entities.Tasks;

[Description("Task Group")]
public class TaskGroup : ActivateEntity<TaskGroup, long>
{
    [Description(TasksCmts.GroupName)]
    public string Title { get; private set; } = string.Empty;
    [Description(TasksCmts.Description)]
    public string? Description { get; private set; }

    private List<UserTask> _userTasks;
    public IReadOnlyList<UserTask> UserTasks => _userTasks;

    public TaskGroup(
        string title,
        string? description) : this()
    {
        SetTitle(title);
        SetDescription(description);
    }

    public void SetTitle(string value)
        => Title = Guard.Against.NullOrWhiteSpace(value, nameof(value));

    public void SetDescription(string? value)
        => Description = value;

    private TaskGroup()
    {
        _userTasks = [];
    }
}