
namespace Engineering.Domain.Entities.CostCenters;

[Description(CCenterCmts.CostCenterInformedUser)]
public class CostCenterInformedUser : AuditableEntity<CostCenterInformedUser, long>
{
    [Description(CCenterCmts.EmployeeId)]
    public long EmployeeId { get; private set; } = default;

    [Description(GlobalCmts.CostCenter)]
    public long CostCenterId { get; private set; }
    public CostCenter CostCenter { get; private set; }

    public CostCenterInformedUser(
        CostCenter costCenter,
        long employeeId) : this()
    {
        SetCostCenter(costCenter);
        SetEmployeeId(employeeId);
    }

    #region Set data

    public void SetEmployeeId(long value)
    {
        EmployeeId = Guard.Against.Null(value, nameof(value));
    }
    public void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CostCenterInformedUser() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
