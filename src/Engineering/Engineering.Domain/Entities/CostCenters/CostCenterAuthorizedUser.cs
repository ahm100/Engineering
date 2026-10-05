
namespace Engineering.Domain.Entities.CostCenters;

[Description(CCenterCmts.CostCenterAuthorizedUser)]
public class CostCenterAuthorizedUser : AuditableEntity<CostCenterAuthorizedUser, long>
{
    [Description(CCenterCmts.AuthorizedUserId)]
    public long AuthorizedUserId { get; private set; } = default;

    [Description(GlobalCmts.CostCenter)]
    public long CostCenterId { get; private set; }
    public CostCenter CostCenter { get; private set; }

    public CostCenterAuthorizedUser(
        CostCenter costCenter,
        long authorizedUserId) : this()
    {
        SetCostCenter(costCenter);
        SetAuthorizedUserId(authorizedUserId);
    }

    #region Set data

    public void SetAuthorizedUserId(long value)
    {
        AuthorizedUserId = Guard.Against.Null(value, nameof(value));
    }
    public void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
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
    private CostCenterAuthorizedUser() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
