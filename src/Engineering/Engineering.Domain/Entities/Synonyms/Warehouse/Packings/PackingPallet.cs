using Engineering.Domain.Entities.Synonyms.Warehouse.Packages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

[NotMapped]
public class ViewPackingPallet : AuditableEntity<ViewPackingPallet>
{
    public string Number { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public bool IsOpen { get; set; }
    public PalletStatus PalletStatus { get; set; }
    public string? Description { get; set; }
    public PalletPermitStatus PalletPermitStatus { get; private set; } = PalletPermitStatus.Pending;
    public long? PermitNumber { get; private set; }
    public DateTime? PermitDate { get; private set; }

    public long PackingId { get; set; }
    public ViewPacking Packing { get; set; }

    public long? PackingContainerId { get; set; }
    public ViewPackingContainer? PackingContainer { get; set; }

    public long? PackagingSpecId { get; set; }
    public ViewPackagingSpec? PackagingSpec { get; set; }

    //private readonly List<ViewPackingProduct> _packingProducts;
    public virtual ICollection<ViewPackingProduct> PackingProducts { get; set; }

    private ViewPackingPallet()
    {
    }

}
public enum PalletStatus
{
    [Description("خالی")]
    Empty = 5,

    [Description("پر")]
    Full = 10,

    [Description("بخشی پر")]
    PartFull = 15,

    [Description("آزاد")]
    Free = 20,
}
public enum PalletPermitStatus
{
    [Display(Name = "Pending")]
    [Description("در انتظار صدور مجوز")]
    Pending = 1,

    [Display(Name = "Permit")]
    [Description("صدور مجوز")]
    Permit = 1,

    [Display(Name = "Permit Confirmed")]
    [Description("تایید مجوز")]
    PermitConfirmed = 10,
}