using Gita.Backend.Shared.Domain.Enums.SaleChannels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

[NotMapped]
public class ViewPacking : AuditableEntity<ViewPacking>
{
    public long? RequestNumber { get; set; }
    public PackingStatus Status { get; set; }
    public PackingType Type { get; set; } = PackingType.Entry;
    public DateTime? DeliveryDate { get; set; }
    public long? IssuingCompany { get; set; }
    public long? ImporterCompany { get; set; }
    public long? SupplierId { get; set; }
    public bool IsClosed { get; set; }
    public long CompanyId { get; set; }
    public string? Description { get; set; }
    public string? ExtraDescription { get; set; }
    public long? LegacyId { get; set; }
    public long? CommercPackId { get; set; }
    public bool SecurityConfirm { get; set; }
    public DateTime? SecurityConfirmDate { get; set; }
    public SalesChannelType? SalesChannelType { get; set; }

    public virtual ICollection<ViewPackingShippingDetail> PackingShippingDetails { get; set; }

    //private readonly List<ViewPackingProduct> _packingProducts;
    public virtual ICollection<ViewPackingProduct> PackingProducts { get; set; }

    //private readonly List<ViewPackingAddress> _packingAddress;
    public virtual ICollection<ViewPackingAddress> PackingAddress { get; set; }

    // private readonly List<ViewPackingContainer> _packingContainers;
    public virtual ICollection<ViewPackingContainer> PackingContainers { get; set; }

    //private readonly List<ViewPackingPallet> _packingPallets;
    public virtual ICollection<ViewPackingPallet> PackingPallets { get; set; }

    private ViewPacking()
    {
    }
}
public enum PackingType
{
    [Description("ورود")]
    Entry = 1,

    [Description("خروج")]
    Exit = 2,

    [Description("آیتمی")]
    Items = 3,

    [Description("پکینگ سیستمی")]
    AutoPacking = 10,

}

public enum PackingStatus
{
    [Description("در حال پردازش")]
    PackingInProgress = 0,

    [Description("ارسال نشده")]
    NotSent = 1,

    [Description("تخصیص راننده")]
    DriverAssignment = 2,

    [Description("آماده ارسال شده")]
    ReadyForSend = 3,

    [Description("تحویل راننده")]
    DriverDelivery = 4,

    [Description("برگشت داده شده")]
    Return = 7,

    [Description("QC")]
    Checked = 8,

    [Description("حراست")]
    Protection = 9,

    [Description("در انتظار ورود")]
    PendingEntry = 10,

    [Description("تحویل کامل")]
    CompleteDelivery = 5,

    [Description("تحویل ناقص")]
    InCompleteDelivery = 6,

    [Description("ارسال شده")]
    Sent = 11,

    [Description("برگشت به بازرگانی")]
    ReturnToCommerce = 15,
}