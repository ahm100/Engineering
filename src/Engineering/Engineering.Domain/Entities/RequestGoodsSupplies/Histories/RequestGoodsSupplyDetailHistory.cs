using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

/// <summary>
/// تاریخچه کالاهای ثبت شده درخواست تامین کالا
/// </summary>
public class RequestGoodsSupplyDetailHistory : AuditableEntity<RequestGoodsSupplyDetailHistory>
{
    [Description(GlobalCmts.Status)]
    public GoodsSupplyDetailStatus Status { get; private set; }

    [Description(RGSCmts.RequestedCount)]
    public decimal RequestedCount { get; private set; }

    [Description(RGSCmts.DelivaryDeadLine)]
    public DateTime? DelivaryDeadLine { get; private set; }

    [Description(RGSCmts.UnitPrice)]
    public decimal? UnitPrice { get; private set; }

    [Description(RGSCmts.TotalPrice)]
    public decimal? TotalPrice { get; private set; }

    [Description(RGSCmts.DiscountedPrice)]
    public decimal? DiscountedPrice { get; private set; }

    [Description(RGSCmts.TaxPercentage)]
    public decimal? TaxPercentage { get; private set; }

    [Description(RGSCmts.TaxNumber)]
    public decimal? TaxNumber { get; private set; }

    [Description(RGSCmts.DiscountByNumber)]
    public decimal? DiscountByNumber { get; private set; }

    [Description(RGSCmts.DiscountByPercentage)]
    public decimal? DiscountByPercentage { get; private set; }

    [Description(RGSCmts.PackingPrice)]
    public decimal? PackingPrice { get; private set; }

    [Description(RGSCmts.FinalPrice)]
    public decimal? FinalPrice { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(RGSCmts.ManagementDescription)]
    public string? ManagementDescription { get; private set; }

    [Description(RGSCmts.CustomerInvoiceNumber)]
    public string? CustomerInvoiceNumber { get; private set; }

    [Description(RGSCmts.RequestGoodsSupplyDetail)]
    public long RequestGoodsSupplyDetailId { get; private set; }
    public RequestGoodsSupplyDetail RequestGoodsSupplyDetail { get; private set; }

    public RequestGoodsSupplyDetailHistory(RequestGoodsSupplyDetail requestGoodsSupplyDetail,
        GoodsSupplyDetailStatus status,
        decimal requestedCount,
        decimal? unitPrice,
        decimal? totalPrice,
        decimal? discountByNumber,
        decimal? discountByPercentage,
        decimal? discountedPrice,
        decimal? taxNumber,
        decimal? taxPercentage,
        decimal? packingPrice,
        decimal? finalPrice,
        DateTime? delivaryDeadLine,
        string? description,
        string? managementDescription,
        string? customerInvoiceNumber) : this()
    {
        RequestGoodsSupplyDetail = Guard.Against.Null(requestGoodsSupplyDetail);
        Status = Guard.Against.Null(status);
        RequestedCount = Guard.Against.Null(requestedCount, nameof(requestedCount));
        DelivaryDeadLine = delivaryDeadLine;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
        DiscountByNumber = discountByNumber;
        DiscountByPercentage = discountByPercentage;
        DiscountedPrice = discountedPrice;
        TaxNumber = taxNumber;
        TaxPercentage = taxPercentage;
        PackingPrice = packingPrice;
        FinalPrice = finalPrice;
        Description = description;
        ManagementDescription = managementDescription;
        CustomerInvoiceNumber = customerInvoiceNumber;
    }

    public RequestGoodsSupplyDetailHistory(RequestGoodsSupplyDetail goodsSupplyDetail,
        GoodsSupplyDetailStatus status,
        decimal requestedCount,
        decimal? unitPrice,
        decimal? totalPrice,
        decimal? discountByNumber,
        decimal? discountByPercentage,
        decimal? discountedPrice,
        decimal? taxNumber,
        decimal? taxPercentage,
        decimal? packingPrice,
        decimal? finalPrice,
        DateTime? delivaryDeadLine,
        long? userId,
        string? description,
        string? managementDescription,
        string? customerInvoiceNumber) : this()
    {
        SetRequestGoodsSupplyDetail(goodsSupplyDetail);
        SetStatus(status);
        SetRequestedCount(requestedCount);
        SetDelivaryDeadLine(delivaryDeadLine);
        SetUnitPrice(unitPrice);
        SetTotalPrice(totalPrice);
        SetDiscountByNumber(discountByNumber);
        SetDiscountByPercentage(discountByPercentage);
        SetDiscountedPrice(discountedPrice);
        SetTaxNumber(taxNumber);
        SetTaxPercentage(taxPercentage);
        SetPackingPrice(packingPrice);
        SetFinalPrice(finalPrice);
        SetDescription(description);
        SetManagementDescription(managementDescription);
        CreatorId = userId ?? 0;
        CheckUser = true;
        Created = DateTime.UtcNow;
        SetCustomerInvoiceNumber(customerInvoiceNumber);
    }

    #region Commands

    public static RequestGoodsSupplyDetailHistory Create(
        RequestGoodsSupplyDetail goodsSupplyDetail,
        GoodsSupplyDetailStatus status,
        decimal requestedCount,
        decimal? unitPrice,
        decimal? totalPrice,
        decimal? discountByNumber,
        decimal? discountByPercentage,
        decimal? discountedPrice,
        decimal? taxNumber,
        decimal? taxPercentage,
        decimal? packingPrice,
        decimal? finalPrice,
        DateTime? delivaryDeadLine,
        string? description,
        string? managementDescription,
        string? customerInvoiceNumber)
    {
        return new RequestGoodsSupplyDetailHistory(
            goodsSupplyDetail,
            status,
            requestedCount,
            unitPrice,
            totalPrice,
            discountByNumber,
            discountByPercentage,
            discountedPrice,
            taxNumber,
            taxPercentage,
            packingPrice,
            finalPrice,
            delivaryDeadLine,
            description,
            managementDescription,
            customerInvoiceNumber);
    }

    public static RequestGoodsSupplyDetailHistory Create(RequestGoodsSupplyDetail goodsSupplyDetail,
        GoodsSupplyDetailStatus status,
        decimal requestedCount,
        decimal? unitPrice,
        decimal? totalPrice,
        decimal? discountByNumber,
        decimal? discountByPercentage,
        decimal? discountedPrice,
        decimal? taxNumber,
        decimal? taxPercentage,
        decimal? packingPrice,
        decimal? finalPrice,
        DateTime? delivaryDeadLine,
        long? userId,
        string? description,
        string? managementDescription,
        string? customerInvoiceNumber)
    {
        return new RequestGoodsSupplyDetailHistory(
            goodsSupplyDetail,
            status,
            requestedCount,
            unitPrice,
            totalPrice,
            discountByNumber,
            discountByPercentage,
            discountedPrice,
            taxNumber,
            taxPercentage,
            packingPrice,
            finalPrice,
            delivaryDeadLine,
            userId,
            description,
            managementDescription,
            customerInvoiceNumber);
    }

    #endregion

    public void SetRequestGoodsSupplyDetail(RequestGoodsSupplyDetail requestGoodsSupplyDetail)
    {
        RequestGoodsSupplyDetail = Guard.Against.Null(
            requestGoodsSupplyDetail,
            nameof(requestGoodsSupplyDetail));
    }

    public void SetStatus(GoodsSupplyDetailStatus status)
    {
        Status = Guard.Against.Null(status, nameof(status));
    }

    public void SetRequestedCount(decimal requestedCount)
    {
        RequestedCount = Guard.Against.Null(
            requestedCount,
            nameof(requestedCount));
    }

    public void SetDelivaryDeadLine(DateTime? delivaryDeadLine)
    {
        DelivaryDeadLine = delivaryDeadLine;
    }

    public void SetUnitPrice(decimal? unitPrice)
    {
        UnitPrice = unitPrice;
    }

    public void SetTotalPrice(decimal? totalPrice)
    {
        TotalPrice = totalPrice;
    }

    public void SetDiscountByNumber(decimal? discountByNumber)
    {
        DiscountByNumber = discountByNumber;
    }

    public void SetDiscountByPercentage(decimal? discountByPercentage)
    {
        DiscountByPercentage = discountByPercentage;
    }

    public void SetDiscountedPrice(decimal? discountedPrice)
    {
        DiscountedPrice = discountedPrice;
    }

    public void SetTaxNumber(decimal? taxNumber)
    {
        TaxNumber = taxNumber;
    }

    public void SetTaxPercentage(decimal? taxPercentage)
    {
        TaxPercentage = taxPercentage;
    }

    public void SetPackingPrice(decimal? packingPrice)
    {
        PackingPrice = packingPrice;
    }

    public void SetFinalPrice(decimal? finalPrice)
    {
        FinalPrice = finalPrice;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetManagementDescription(string? managementDescription)
    {
        ManagementDescription = managementDescription;
    }

    public void SetCustomerInvoiceNumber(string? customerInvoiceNumber)
    {
        CustomerInvoiceNumber = customerInvoiceNumber;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestGoodsSupplyDetailHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
