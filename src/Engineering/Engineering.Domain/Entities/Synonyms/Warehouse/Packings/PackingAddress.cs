using Engineering.Domain.Entities.Synonyms.Warehouse.Warehouses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

[NotMapped]
public class ViewPackingAddress : AuditableEntity<ViewPackingAddress>
{
    public AddressType Type { get; set; }
    public int Priority { get; set; }
    public long? ProvinceId { get; set; }
    public long? CityId { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PostalCode { get; set; }

    public long? WarehouseId { get; set; }
    [ForeignKey("WarehouseId")]
    public ViewWarehouse? Warehouse { get; set; }
    public long? PackingId { get; set; }
    [ForeignKey("PackingId")]
    public ViewPacking? Packing { get; set; }

    // private readonly List<ViewPackingProduct> _packingProducts;
    public virtual ICollection<ViewPackingProduct> PackingProducts { get; set; }

    private ViewPackingAddress()
    {
    }
}
public enum AddressType
{
    [Description("مبدا")]
    Source = 1,

    [Description("مقصد")]
    Destination = 2
}
