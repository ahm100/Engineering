
namespace Engineering.Domain.Entities.CostCenters;

[Description(CCenterCmts.CostCenterVirtualGroup)]
public class CostCenterWarehouse : AuditableEntity<CostCenterWarehouse, long>
{
    [Description(CCenterCmts.WarehouseId)]
    public long WarehouseId { get; private set; } = default;
    [Description(CCenterCmts.IsDefault)]
    public bool IsDefault { get; private set; } = false;

    [Description(GlobalCmts.CostCenter)]
    public long CostCenterId { get; private set; }
    public CostCenter CostCenter { get; private set; }

    public CostCenterWarehouse(
        CostCenter costCenter,
        long warehouseId,
        bool isDefault) : this()
    {
        SetCostCenter(costCenter);
        SetWarehouseId(warehouseId);
        SetIsDefault(isDefault);
    }

    #region Set data

    public void SetWarehouseId(long value)
    {
        WarehouseId = Guard.Against.Null(value, nameof(value));
    }
    public void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetIsDefault(bool value)
    {
        IsDefault = Guard.Against.Null(value, nameof(value));
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CostCenterWarehouse() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
