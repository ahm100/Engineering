using Engineering.Domain.Entities.ProjectOperations;
using Action = Engineering.Domain.Entities.Actions.Action;

namespace Engineering.Domain.Entities.OperationInfos;

public class OperationInfoAction : AuditableEntity<OperationInfoAction>
{
    [Description(GlobalCmts.OperationInfo)]
    public OperationInfo OperationInfo { get; set; }
    public long OperationInfoId { get; set; }
    [Description(GlobalCmts.Action)]
    public Action Action { get; set; }
    public long ActionId { get; set; }
    [Description(ProjectOperationCmts.Price)]
    public decimal? Price { get; private set; } = 0;

    public OperationInfoAction(OperationInfo operationInfo,
        Action action,
        decimal? price) : this()
    {
        SetAction(action);
        SetOperationInfo(operationInfo);
        SetPrice(price);
    }

    #region Set data 

    public void SetOperationInfo(OperationInfo value)
    {
        OperationInfo = Guard.Against.Null(value, nameof(value));
        OperationInfoId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetAction(Action value)
    {
        Action = Guard.Against.Null(value, nameof(value));
        ActionId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetPrice(decimal? value)
    {
        Price = value;
    }

    public void Update(decimal? value)
    {
        Price = value;
    }
    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<ProjectOperationAction> _projectOperationAction;
    public IReadOnlyList<ProjectOperationAction> ProjectOperationActions => _projectOperationAction;
    private OperationInfoAction()
    {
        _projectOperationAction = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}

