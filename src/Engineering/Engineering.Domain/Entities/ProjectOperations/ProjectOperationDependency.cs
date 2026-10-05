using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Domain.Entities.ProjectOperations;

[Description(ProjectOperationCmts.ProjectOperationDependency)]
public class ProjectOperationDependency : AuditableEntity<ProjectOperationDependency>
{
    [Description(ProjectOperationCmts.LagDays)]
    public int LagDays { get; private set; } = 0;

    [Description(GlobalCmts.Type)]
    public ProjectOperationDependencyType DependencyType { get; private set; } = ProjectOperationDependencyType.FS;

    [Description(GlobalCmts.ProjectOperation)]
    public long PredecessorId { get; set; }
    public ProjectOperation Predecessor { get; set; }

    [Description(GlobalCmts.ProjectOperation)]
    public long SuccessorId { get; set; }
    public ProjectOperation Successor { get; set; }

    public ProjectOperationDependency(ProjectOperation predecessorProjectOperation,
        ProjectOperation successorProjectOperation,
        int lagDays,
        ProjectOperationDependencyType dependencyType) : this()
    {
        SetSuccessorProjectOperation(successorProjectOperation);
        SetPredecessorProjectOperation(predecessorProjectOperation);
        SetLagDays(lagDays);
        SetDependencyType(dependencyType);
    }

    public void Update(ProjectOperation? predecessorProjectOperation,
        ProjectOperation? successorProjectOperation,
        int? lagDays,
        ProjectOperationDependencyType? dependencyType)
    {
        SetSuccessorProjectOperation(successorProjectOperation ?? Successor);
        SetPredecessorProjectOperation(predecessorProjectOperation ?? Predecessor);
        SetLagDays(lagDays ?? LagDays);
        SetDependencyType(dependencyType ?? DependencyType);
    }

    #region Set data

    public void SetSuccessorProjectOperation(ProjectOperation value)
    {
        Successor = Guard.Against.Null(value, nameof(value));
        SuccessorId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetPredecessorProjectOperation(ProjectOperation value)
    {
        Predecessor = Guard.Against.Null(value, nameof(value));
        PredecessorId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetLagDays(int value)
    {
        LagDays = Guard.Against.Null(value, nameof(value));
    }
    public void SetDependencyType(ProjectOperationDependencyType value)
    {
        DependencyType = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectOperationDependency() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
