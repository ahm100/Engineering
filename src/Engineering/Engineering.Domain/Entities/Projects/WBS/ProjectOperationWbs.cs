using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Domain.Entities.Projects.WBS;

[Description(WbsCmts.ProjectOperationWbs)]
public class ProjectOperationWbs : ActivateEntity<ProjectOperationWbs, long>
{
    [Description(GlobalCmts.ProjectOperation)]
    public long ProjectOperationId { get; private set; }
    public ProjectOperation ProjectOperation { get; private set; }

    [Description(WbsCmts.ProjectWbs)]
    public long ProjectWbsId { get; private set; }
    public ProjectWbs ProjectWbs { get; private set; }

    public ProjectOperationWbs(ProjectOperation projectOperation,
        ProjectWbs projectWbs,
        bool isActive) : this()
    {
        SetProjectOperation(projectOperation);
        SetProjectWbs(projectWbs);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public void Update(ProjectOperation? projectOperation,
        ProjectWbs? projectWbs,
        bool? isActive)
    {
        SetProjectOperation(projectOperation ?? ProjectOperation);
        SetProjectWbs(projectWbs ?? ProjectWbs);
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

    public void SetProjectWbs(ProjectWbs value)
    {
        ProjectWbs = Guard.Against.Null(value, nameof(value));
        ProjectWbsId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectOperationWbs() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}