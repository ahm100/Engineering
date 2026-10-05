namespace Engineering.Domain.Entities.CostCenters;

public class CostCenterVirtualGroupAdmin : AuditableEntity<CostCenterVirtualGroupAdmin, long>
{

    #region Properties

    [Description(CCenterCmts.ThirdPartyId)]
    public long ThirdPartyId { get; private set; } = default;

    [Description(CCenterCmts.UserName)]
    public string UserName { get; private set; } = string.Empty;
    [Description(CCenterCmts.CostCenterVirtualGroupId)]
    public long CostCenterVirtualGroupId { get; private set; }
    [Description(CCenterCmts.CostCenterVirtualGroup)]
    public CostCenterVirtualGroup CostCenterVirtualGroup { get; private set; }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CostCenterVirtualGroupAdmin() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public CostCenterVirtualGroupAdmin(
        long thirdPartyId,
        string userName,
        CostCenterVirtualGroup costCenterVirtualGroup) : this()
    {
        ThirdPartyId = Guard.Against.Null(thirdPartyId, nameof(thirdPartyId));
        UserName = Guard.Against.Null(userName, nameof(userName));
        CostCenterVirtualGroup = Guard.Against.Null(costCenterVirtualGroup, nameof(costCenterVirtualGroup));
    }

    #endregion

    #region Commands

    public void SetThirdPartyId(long value)
    {
        ArgumentNullException.ThrowIfNull(value);

        ThirdPartyId = value;
    }

    public void SetUserName(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        UserName = value;
    }

    public void SetCostCenterVirtualGroup(CostCenterVirtualGroup value)
    {
        CostCenterVirtualGroup = Guard.Against.Null(value, nameof(value));
        CostCenterVirtualGroupId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion
}
