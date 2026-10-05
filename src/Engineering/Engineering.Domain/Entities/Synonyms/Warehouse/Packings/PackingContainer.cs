using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

[NotMapped]
public class ViewPackingContainer : AuditableEntity<ViewPackingContainer>
{
    public ContainerType Type { get; private set; }
    public string ContainerNumber { get; private set; } = default!;
    public decimal PackageNetWeight { get; private set; }
    public decimal PackageGrossWeight { get; private set; }
    public string? Description { get; private set; }

    public long PackingId { get; private set; }
    public ViewPacking Packing { get; private set; } = default!;

    //private readonly List<ViewPackingProduct> _packingProducts;
    public virtual ICollection<ViewPackingProduct> PackingProducts { get; set; }

    //private readonly List<ViewPackingPallet> _packingPallets;
    public virtual ICollection<ViewPackingPallet> PackingPallets { get; set; }
    private ViewPackingContainer()
    {
    }

}
public enum ContainerType
{
    [Description("20 Foot Dc")]
    DC20Foot = 1,

    [Description("40 Foot DV")]
    DV40Foot = 2,

    [Description("40 Foot PW")]
    PW40Foot = 3,

    [Description("45 Foot HCPW")]
    HCPW45Foot = 4
}
