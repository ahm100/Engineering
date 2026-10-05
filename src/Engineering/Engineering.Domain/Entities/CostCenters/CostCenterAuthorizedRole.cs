
namespace Engineering.Domain.Entities.CostCenters;

[Description(CCenterCmts.CostCenterAuthorizedRole)]

public class CostCenterAuthorizedRole : AuditableEntity<CostCenterAuthorizedRole, long>
{
    [Description(CCenterCmts.AuthorizedRoleId)]
    public long AuthorizedRoleId { get; private set; } = default;

    [Description(GlobalCmts.CostCenter)]
    public long CostCenterId { get; private set; }
    public CostCenter CostCenter { get; private set; }

    public CostCenterAuthorizedRole(
        CostCenter costCenter,
        long authorizedRoleId) : this()
    {
        SetCostCenter(costCenter);
        SetAuthorizedRoleId(authorizedRoleId);
    }

    #region Set data

    public void SetAuthorizedRoleId(long value)
    {
        AuthorizedRoleId = Guard.Against.Null(value, nameof(value));
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
    private CostCenterAuthorizedRole() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
