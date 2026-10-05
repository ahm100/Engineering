using Engineering.Domain.Entities.Synonyms.Warehouse.Packages;
using Engineering.Domain.Entities.Synonyms.Warehouse.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

[NotMapped]
public class ViewPackingProduct : AuditableEntity<ViewPackingProduct>
{
    public PackingProductStatus Status { get; set; } = PackingProductStatus.PendingEntry;
    public decimal Quantity { get; set; }
    public decimal QcQuantity { get; set; }
    public decimal RemainingQuantity { get; set; } = 0;
    public decimal PackageQuantity { get; set; }
    public decimal? RealRequestCount { get; set; }
    public long? CommerceRequestId { get; set; }
    public long? CostCenterId { get; set; }
    public long? ProjectId { get; set; }
    public bool IsNewInPacking { get; set; } = false;
    public long? CommerceNo { get; set; }
    public long? PreInvoiceNo { get; set; }
    public long? CurrencyId { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? CurrencyRate { get; set; }
    public decimal? Tax { get; set; }
    public decimal? ShippingPrice { get; set; }
    public decimal? Discount { get; set; }
    public decimal? PackagingPrice { get; set; }
    public decimal? Other { get; set; }
    public long? PackageId { get; set; }
    [ForeignKey("PackageId")]
    public ViewPackage? Package { get; set; }
    public long ProductId { get; set; }
    [ForeignKey("ProductId")]
    public ViewProduct Product { get; set; }
    public long? SourcePackingAddressId { get; set; }
    [ForeignKey("SourcePackingAddressId")]
    public ViewPackingAddress? SourcePackingAddress { get; set; }
    public long DestinationPackingAddressId { get; set; }
    [ForeignKey("DestinationPackingAddressId")]
    public ViewPackingAddress DestinationPackingAddress { get; set; }
    public long PackingId { get; set; }
    [ForeignKey("PackingId")]
    public virtual ViewPacking Packing { get; set; }
    public long? PackingContainerId { get; set; }
    [ForeignKey("PackingContainerId")]
    public ViewPackingContainer? PackingContainer { get; set; }
    public long? PackingPalletId { get; set; }
    [ForeignKey("PackingPalletId")]
    public ViewPackingPallet? PackingPallet { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ViewPackingProduct()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

}
public enum PackingProductStatus
{
    [Description("در انتظار ورود")]
    PendingEntry = 1,

    [Description("ورود ناقض")]
    InCompleteEntry = 5,

    [Description("ورود کامل")]
    CompleteEntry = 10,

    [Description("بدونه ورود")]
    NoEntry = 15
}