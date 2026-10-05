using Engineering.Domain.Entities.Synonyms.MetaData.Addresses;
using Engineering.Domain.Entities.Synonyms.Warehouse.WarehouseAssets;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Warehouses;

public class ViewWarehouse : ActivateEntity<ViewWarehouse>
{
    public long ManagerId { get; private set; }
    public long? OwnerId { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? Contact { get; private set; }
    public double? MinimumTemperature { get; private set; }
    public double? MaximumTemperature { get; private set; }
    public bool? IsMain { get; private set; }
    public bool? IsReference { get; private set; }
    public long CompanyId { get; private set; }
    public Guid PreferentialReferenceCode { get; private set; }
    public long? LegacyId { get; private set; }
    public long WarehouseTypeId { get; private set; }
    public long? WarehouseNatureId { get; private set; }
    public long AddressId { get; private set; }
    public ViewAddress Address { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private readonly IList<ViewWarehouseAsset> _warehouseAssets;
    public IEnumerable<ViewWarehouseAsset> WarehouseAsset => _warehouseAssets.AsReadOnly();
    private ViewWarehouse()
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _warehouseAssets = [];
    }
}