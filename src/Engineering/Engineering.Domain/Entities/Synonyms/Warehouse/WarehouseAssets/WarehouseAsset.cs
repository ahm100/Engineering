using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;
using Engineering.Domain.Entities.Synonyms.Warehouse.Warehouses;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.WarehouseAssets;

public class ViewWarehouseAsset : ActivateEntity<ViewWarehouseAsset>
{
    public long GroupId { get; private set; }
    public ViewGroup Group { get; private set; }
    public long WarehouseId { get; private set; }
    public ViewWarehouse Warehouse { get; private set; }
    public int MinimumInventory { get; private set; }
    public int MaximumInventory { get; private set; }
    public int OrderPoint { get; private set; }
    public int OptimalNumberOfOrder { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ViewWarehouseAsset()
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}