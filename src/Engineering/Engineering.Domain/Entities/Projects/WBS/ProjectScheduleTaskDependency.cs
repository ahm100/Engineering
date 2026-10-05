using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Domain.Entities.Projects.WBS;

[Description(WbsCmts.ProjectScheduleTaskDependency)]
public class ProjectScheduleTaskDependency : ActivateEntity<ProjectScheduleTaskDependency, long>
{
    [Description(WbsCmts.ProjectScheduleDependencyType)]
    public ProjectScheduleDependencyType Type { get; private set; }

    [Description(WbsCmts.LagMinutes)]
    public long LagMinutes { get; private set; }

    [Description(WbsCmts.PredecessorTask)]
    public long PredecessorTaskId { get; private set; }
    public ProjectScheduleTask PredecessorTask { get; private set; }

    [Description(WbsCmts.SuccessorTask)]
    public long SuccessorTaskId { get; private set; }
    public ProjectScheduleTask SuccessorTask { get; private set; }

    public ProjectScheduleTaskDependency(
        ProjectScheduleTask predecessor,
        ProjectScheduleTask successor,
        ProjectScheduleDependencyType type,
        long lagMinutes) : this()
    {
        PredecessorTask = predecessor;
        PredecessorTaskId = predecessor.Id;

        SuccessorTask = successor;
        SuccessorTaskId = successor.Id;

        Type = type;
        LagMinutes = lagMinutes;

        SetActive();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectScheduleTaskDependency()
    {
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}