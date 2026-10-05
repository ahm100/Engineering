namespace Engineering.Domain.Entities.ProjectOperations;

public class ProjectOperationAction : AuditableEntity<ProjectOperationAction>
{
    [Description(ProjectOperationCmts.OperationInfoAction)]
    public OperationInfoAction OperationInfoAction { get; set; }
    public long OperationInfoActionId { get; set; }
    [Description(ProjectOperationCmts.ProjectOperation)]
    public ProjectOperation ProjectOperation { get; set; }
    public long ProjectOperationId { get; set; }
    [Description(ProjectOperationCmts.Price)]
    public decimal? Price { get; private set; } = 0;

    public ProjectOperationAction(
        ProjectOperation projectOperation,
        OperationInfoAction operationInfoAction) : this()
    {
        SetProjectOperation(projectOperation);
        SetOperationInfoAction(operationInfoAction);
        SetPrice();
    }

    #region Set data 

    public void SetOperationInfoAction(OperationInfoAction value)
    {
        OperationInfoAction = Guard.Against.Null(value, nameof(value));
        OperationInfoActionId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProjectOperation(ProjectOperation value)
    {
        ProjectOperation = Guard.Against.Null(value, nameof(value));
        ProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetPrice()
    {
        Price = OperationInfoAction.Price;
    }

    public void Update(decimal? value)
    {
        Price = value;
    }
    #endregion

    #region Methods 

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectOperationAction()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}