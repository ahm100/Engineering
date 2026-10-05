using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Synonyms.Warehouse.Warehouses;
using Gita.Backend.Shared.Domain.Enums.SaleChannels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

[NotMapped]
public class ViewInvoice : AuditableEntity<ViewInvoice>
{
    public long? TransportationContractorId { get; private set; }

    public DeliveryMethod? DeliveryMethod { get; private set; }

    public DeliveryType? DeliveryType { get; private set; }

    public string Code { get; set; }

    public long? FactorNumber { get; set; }

    public long? PackingNumber { get; set; }

    public InvoiceType InvoiceType { get; set; }

    public InvoiceStatus Status { get; set; }

    public ImportanceDegree? Importance { get; set; }

    public long? CommercialRequestId { get; set; }

    public string? CommercialRequestNo { get; set; }

    public long? OwnerId { get; set; }

    public string? OwnerName { get; set; }

    public long? UserRegisterId { get; set; }

    public long? RecipientId { get; set; }

    public string? RecipientName { get; set; }

    public bool IsManually { get; set; }

    public Guid TrackingCode { get; set; }

    public DateTime? InputDate { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public bool? IsComplete { get; set; }

    public string? Description { get; set; }

    public string? ChangeStatusDescription { get; set; }

    public SalesChannelType? SalesChannelType { get; set; }

    public long? OrdererCompanyId { get; set; }

    public string? ReferringTo { get; set; }

    public long CompanyId { get; set; }

    public long WarehouseId { get; set; }

    public ViewWarehouse Warehouse { get; set; }

    public long? DestinationWarehouseId { get; set; }

    public ViewWarehouse? DestinationWarehouse { get; set; }

    public long? FinalWarehouseId { get; set; }

    public ViewWarehouse? FinalWarehouse { get; set; }

    public long? ParentId { get; set; }

    public ViewInvoice? Parent { get; set; }

    public long? ReturnedTypeId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    //    private readonly List<ViewInvoice> _childInvoices;
    //    public IReadOnlyList<ViewInvoice> ChildInvoices => _childInvoices;

    //    private readonly List<ViewInvoiceProduct> _invoiceProducts;
    public virtual ICollection<ViewInvoiceProduct> InvoiceProducts { get; set; }

    public ViewInvoice()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}
public enum InvoiceType
{
    // Enter
    [Description("ورود از خرید")]
    EntryThroughPurchase = 0,

    [Description("ورود بین انباری")]
    EntryThroughRelocation = 2,

    [Description("ورود ضایعات")]
    EntryThroughWastage = 4,

    [Description("ورود امانی")]
    EntryThroughBorrow = 6,

    [Description("ورود مرجوعی")]
    EntryThroughReturned = 8,

    [Description("ورود از تحویل موقت")]
    EntryThroughTemporaryDelivery = 10,

    [Description("ورود اول دوره")]
    EntryThroughFirstPeriodEntryFromInventory = 12,

    [Description("سند اصلاحیه ورود")]
    EntryAdjustmentDocument = 14,

    [Description("ورود بین انباری برای تحویل موقت")]
    EntryThroughRelocationForTemporaryDelivery = 16,

    [Description("مرجوع تحویل موقت")]
    EntryThroughReturnedTemporaryDelivery = 18,

    [Description("ورود تبدیل موجودی")]
    EntryConvertPackage = 20,

    // Exit
    [Description("خروج فروش")]
    ExitThroughSell = 1,

    [Description("خروج مصرفی")]
    ExitForConsume = 3,

    [Description("خروج بین انباری")]
    ExitForRelocation = 5,

    [Description("خروج ضایعات")]
    ExitForWastage = 7,

    [Description("خروج امانی")]
    ExitForBorrow = 9,

    [Description("تحویل موقت")]
    ExitForTemporaryDelivery = 11,

    [Description("سند اصلاحیه خروج")]
    ExitAdjustmentDocument = 13,

    [Description("خروج برای تامین کننده")]
    ExitForSupplier = 15,

    [Description("خروج بین انباری برای تحویل موقت")]
    ExitRelocationForTemporaryDelivery = 17,

    [Description("خروج تبدیل موجودی")]
    ExitConvertPackage = 19,
}

public enum InvoiceStatus
{
    [Description("ثبت اولیه")]
    New = 0,

    [Description("در حال بررسی")]
    Pending = 1,

    [Description("تایید و تامین شده")]
    Approved = 2,

    [Description("رد شده")]
    Rejected = 3,

    [Description("کنسل شده")]
    Canceled = 4,

    [Description("برگشت شده")]
    Returned = 5,

    [Description("تحویل ناقص")]
    IncompleteDelivered = 6,

    [Description("ارسال مجدد")]
    Resend = 7
}

public enum ImportanceDegree
{
    [Description("کم")] Low = 0,
    [Description("متوسط")] Medium = 1,
    [Description("زیاد")] High = 2,
    [Description("خیلی زیاد")] VeryHigh = 3
}