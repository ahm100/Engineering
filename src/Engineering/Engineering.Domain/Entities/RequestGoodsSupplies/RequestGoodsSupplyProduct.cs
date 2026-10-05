using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.RequestGoodsSupplies;

/// <summary>
/// فهرست کالاهای ثبت شده درخواست تامین کالا
/// </summary>
public class RequestGoodsSupplyProduct : AuditableEntity<RequestGoodsSupplyProduct>
{
    [Description(RGSCmts.Importance)]
    public GoodsSupplyDetailImportance Importance { get; private set; } = GoodsSupplyDetailImportance.Lowest;

    [Description(RGSCmts.Status)]
    public GoodsSupplyDetailStatus Status { get; private set; } = GoodsSupplyDetailStatus.New;

    [Description(RGSCmts.DelivaryDeadLine)]
    public DateTime? DelivaryDeadLine { get; private set; }

    [Description(RGSCmts.ProductId)]
    public long ProductId { get; private set; }

    [Description(RGSCmts.ProductGroupId)]
    public long ProductGroupId { get; private set; }

    [Description(RGSCmts.PackageId)]
    public long? PackageId { get; private set; }

    [Description(RGSCmts.RequestedCount)]
    public decimal RequestedCount { get; private set; }

    [Description(RGSCmts.UnitPrice)]
    public decimal? UnitPrice { get; private set; }

    [Description(RGSCmts.TotalPrice)]
    public decimal? TotalPrice { get; private set; }

    [Description(RGSCmts.TaxPercentage)]
    public decimal? TaxPercentage { get; private set; }

    [Description(RGSCmts.TaxNumber)]
    public decimal? TaxNumber { get; private set; }

    [Description(RGSCmts.DiscountByPercentage)]
    public decimal? DiscountByPercentage { get; private set; }

    [Description(RGSCmts.DiscountByNumber)]
    public decimal? DiscountByNumber { get; private set; }

    [Description(RGSCmts.DiscountedPrice)]
    public decimal? DiscountedPrice { get; private set; }

    [Description(RGSCmts.TransferPrice)]
    public decimal? TransferPrice { get; private set; }

    [Description(RGSCmts.FinalPrice)]
    public decimal? FinalPrice { get; private set; }

    [Description(RGSCmts.PackageCount)]
    public decimal? PackageCount { get; private set; }

    [Description(RGSCmts.PackageUnitPrice)]
    public decimal? PackageUnitPrice { get; private set; }

    [Description(RGSCmts.PackingPrice)]
    public decimal? PackingPrice { get; private set; }

    [Description(RGSCmts.CheckGroup)]
    public bool CheckGroup { get; private set; } = false;

    [Description(RGSCmts.ContractorId)]
    public long? ContractorId { get; private set; }

    [Description(RGSCmts.DestinationWarehouseId)]
    public long? DestinationWarehouseId { get; private set; }

    [Description(RGSCmts.CustomerInvoiceNumber)]
    public string? CustomerInvoiceNumber { get; private set; }

    [Description(RGSCmts.RequestSerialNumber)]
    public string RequestSerialNumber => $"{SerialNumber}-{this.Id}";

    [Description(RGSCmts.SerialNumber)]
    public string SerialNumber { get; private set; }

    [Description(RGSCmts.Description)]
    public string? Description { get; private set; }

    [Description(RGSCmts.ManagementDescription)]
    public string? ManagementDescription { get; private set; }

    [Description(RGSCmts.LastDescription)]
    public string? LastDescription { get; private set; }

    [Description(RGSCmts.IsHistoryAdded)]
    [NotMapped]
    public bool IsHistoryAdded { get; set; } = false;

    [Description(RGSCmts.RequestGoodsSupply)]
    public RequestGoodsSupply RequestGoodsSupply { get; private set; }
    public long RequestGoodsSupplyId { get; private set; }


    public RequestGoodsSupplyProduct(
        RequestGoodsSupply requestGoodsSupply,
        GoodsSupplyDetailImportance importance,
        DateTime? delivaryDeadLine,
        long productId,
        long productGroupId,
        long? packageId,
        decimal requestedCount,
        decimal? unitPrice,
        decimal? totalPrice,
        decimal? taxPercentage,
        decimal? taxNumber,
        decimal? discountByPercentage,
        decimal? discountByNumber,
        decimal? packingPrice,
        decimal? discountedPrice,
        decimal? transferPrice,
        decimal? finalPrice,
        decimal? packageCount,
        decimal? packageUnitPrice,
        bool checkGroup,
        long? contractorId,
        long? destinationWarehouseId,
        string? customerInvoiceNumber,
        string? description,
        string? managementDescription,
        bool isHistoryAdded
        ) : this()
    {
        RequestGoodsSupply = Guard.Against.Null(requestGoodsSupply, nameof(requestGoodsSupply));
        if (RequestGoodsSupply.Status == GoodsSupplyStatus.Draft)
            Status = GoodsSupplyDetailStatus.Draft;
        else
            Status = GoodsSupplyDetailStatus.New;

        SetImportance(importance);
        SetDelivaryDeadLine(delivaryDeadLine);
        SetProductId(productId);
        SetProductGroupId(productGroupId);
        SetPackageId(packageId);
        SetRequestedCount(requestedCount);
        SetUnitPrice(unitPrice);
        SetTotalPrice(totalPrice);
        SetTaxPercentage(taxPercentage);
        SetTaxNumber(taxNumber);
        SetDiscountByPercentage(discountByPercentage);
        SetDiscountByNumber(discountByNumber);
        SetDiscountedPrice(discountedPrice);
        SetTransferPrice(transferPrice);
        SetPackingPrice(packingPrice);
        SetFinalPrice(finalPrice);
        SetPackageCount(packageCount);
        SetPackageUnitPrice(packageUnitPrice);
        SetCheckGroup(checkGroup);
        SetContractorId(contractorId);
        SetCustomerInvoiceNumber(customerInvoiceNumber);
        SetDestinationWarehouseId(destinationWarehouseId);
        SetDescription(description);
        SetManagementDescription(managementDescription);

        //ToDo
        SerialNumber = RequestGoodsSupply.IsProjectSupply ? $"{RequestGoodsSupply.Project!.ProjectCode}" : $"{RequestGoodsSupply.ProjectOperation.Project.ProjectCode}";
        IsHistoryAdded = isHistoryAdded;
    }

    public static RequestGoodsSupplyProduct Create(
        RequestGoodsSupply requestGoodsSupply,
        GoodsSupplyDetailImportance importance,
        DateTime? delivaryDeadLine,
        long productId,
        long productGroupId,
        long? packageId,
        decimal requestedCount,
        decimal? unitPrice,
        decimal? totalPrice,
        decimal? taxPercentage,
        decimal? taxNumber,
        decimal? discountByPercentage,
        decimal? discountByNumber,
        decimal? packingPrice,
        decimal? discountedPrice,
        decimal? transferPrice,
        decimal? finalPrice,
        decimal? packageCount,
        decimal? packageUnitPrice,
        bool checkGroup,
        long? contractorId,
        long? destinationWarehouseId,
        string? customerInvoiceNumber,
        string? description,
        string? managementDescription,
        bool isHistoryAdded)
    {
        return new RequestGoodsSupplyProduct(
            requestGoodsSupply,
            importance,
            delivaryDeadLine,
            productId,
            productGroupId,
            packageId,
            requestedCount,
            unitPrice,
            totalPrice,
            taxPercentage,
            taxNumber,
            discountByPercentage,
            discountByNumber,
            packingPrice,
            discountedPrice,
            transferPrice,
            finalPrice,
            packageCount,
            packageUnitPrice,
            checkGroup,
            contractorId,
            destinationWarehouseId,
            customerInvoiceNumber,
            description,
            managementDescription,
            isHistoryAdded);
    }

    public void SetLastDescription(string? value)
    {
        LastDescription = value;
    }

    public void SetPackageId(long? value)
    {
        PackageId = value;
    }

    public void SetProductId(long value)
    {
        ProductId = value;
    }

    public void SetProductGroupId(long value)
    {
        ProductGroupId = value;
    }

    public void SetDelivaryDeadLine(DateTime? value)
    {
        DelivaryDeadLine = value;
    }

    public void SetDestinationWarehouseId(long? value)
    {
        DestinationWarehouseId = value;
    }

    public void SetRequestedCount(decimal value)
    {
        RequestedCount = value;
    }

    public void SetUnitPrice(decimal? value)
    {
        UnitPrice = value;
    }

    public void SetTransferPrice(decimal? value)
    {
        TransferPrice = value;
    }

    public void SetTotalPrice(decimal? value)
    {
        TotalPrice = value;
    }

    public void SetTaxPercentage(decimal? value)
    {
        TaxPercentage = value;
    }

    public void SetTaxNumber(decimal? value)
    {
        TaxNumber = value;
    }

    public void SetDiscountByPercentage(decimal? value)
    {
        DiscountByPercentage = value;
    }

    public void SetDiscountByNumber(decimal? value)
    {
        DiscountByNumber = value;
    }

    public void SetDiscountedPrice(decimal? value)
    {
        DiscountedPrice = value;
    }

    public void SetFinalPrice(decimal? value)
    {
        FinalPrice = value;
    }

    public void SetPackageUnitPrice(decimal? value)
    {
        PackageUnitPrice = value;
    }

    public void SetPackingPrice(decimal? value)
    {
        PackingPrice = value;
    }

    public void SetPackageCount(decimal? value)
    {
        PackageCount = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetManagementDescription(string? value)
    {
        ManagementDescription = value;
    }

    public void SetCustomerInvoiceNumber(string? value)
    {
        CustomerInvoiceNumber = value;
    }

    public void SetStatusToDraft()
    {
        Status = GoodsSupplyDetailStatus.Draft;
    }

    public void SetStatusToNew()
    {
        Status = GoodsSupplyDetailStatus.New;
        AddHistory();
    }

    public void SetImportance(GoodsSupplyDetailImportance value)
    {
        Importance = Guard.Against.EnumOutOfRange(value, nameof(value));
    }

    public void SetCheckGroup(bool value)
    {
        CheckGroup = Guard.Against.Null(value, nameof(value));
    }

    public void UpdateStatus(GoodsSupplyDetailStatus value, string? lastDescription)
    {
        Status = value;
        LastDescription = lastDescription;

        AddHistory();
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

    public void UpdateCheckGroup(bool value)
    {
        CheckGroup = value;
    }

    public void SetStatus(GoodsSupplyDetailStatus value, string? description)
    {
        Status = value;
        if (!string.IsNullOrEmpty(description))
            SetLastDescription(description);
    }

    public void ChangeStatus(RequestGoodsSupplyProduct value, string? description, long? userId)
    {
        Status = StatusChecker(value);

        AddHistory(userId, description);
    }

    public void AddHistory()
    {
        _requestGoodsSupplyProductHistories.Add(RequestGoodsSupplyProductHistory.Create(this, this.Status, this.LastDescription));
    }

    public void AddHistory(long? userId, string? description)
    {
        _requestGoodsSupplyProductHistories.Add(RequestGoodsSupplyProductHistory.Create(this, this.Status, description, userId));
    }

    public void UpdateStatus(RequestGoodsSupplyProduct value, long? userId, string? lastDescription)
    {
        Status = StatusChecker(value);
        SetLastDescription(lastDescription);
    }

    public GoodsSupplyDetailStatus StatusChecker(RequestGoodsSupplyProduct detail)
    {
        GoodsSupplyDetailStatus status = GoodsSupplyDetailStatus.PendingForSupply;
        var managments = detail.RequestGoodsSupplyManagements.Where(x => x.Status != GoodsSupplyManagementStatus.Return).ToList();
        if (detail.RequestGoodsSupplyManagements.All(oo => oo.Status == GoodsSupplyManagementStatus.Return))
            status = GoodsSupplyDetailStatus.NotCompleteSupply;
        else if (managments.All(oo => oo.Status == GoodsSupplyManagementStatus.CompleteSupply))
            status = GoodsSupplyDetailStatus.CompleteSupply;
        else if (managments.All(oo => oo.Status == GoodsSupplyManagementStatus.CommercialInvoiceConfirmation))
            status = GoodsSupplyDetailStatus.CommercialInvoiceConfirmation;
        else if (managments.All(oo => oo.Status == GoodsSupplyManagementStatus.ReturnToSupply))
            status = GoodsSupplyDetailStatus.ReturnToSupply;
        else if (managments.Any(oo => oo.Status == GoodsSupplyManagementStatus.InCompleteSupply))
            status = GoodsSupplyDetailStatus.InCompleteSupply;
        else if (managments.Any(oo => oo.Status == GoodsSupplyManagementStatus.CommercialInvoiceConfirmation))
            status = GoodsSupplyDetailStatus.CommercialInvoiceConfirmation;
        else if (managments.Any(oo => oo.Status == GoodsSupplyManagementStatus.ReturnToSupply))
            status = GoodsSupplyDetailStatus.InCompleteSupply;
        else if (managments.Any(oo => oo.Status == GoodsSupplyManagementStatus.Return))
            status = GoodsSupplyDetailStatus.InCompleteSupply;
        else if (managments.Any(oo => oo.Status == GoodsSupplyManagementStatus.ReturnToSupply))
            status = GoodsSupplyDetailStatus.InCompleteSupply;
        else if (managments.Any(oo => oo.Status == GoodsSupplyManagementStatus.CompleteSupply))
            status = GoodsSupplyDetailStatus.InCompleteSupply;
        else
            status = GoodsSupplyDetailStatus.PendingForSupply;

        return status;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public IReadOnlyList<RequestGoodsSupplyManagement> RequestGoodsSupplyManagements => _requestGoodsSupplyManagements;
    private List<RequestGoodsSupplyManagement> _requestGoodsSupplyManagements;

    public IReadOnlyList<RequestGoodsSupplyDetail> RequestGoodsSupplyDetails => _requestGoodsSupplyDetails;
    private List<RequestGoodsSupplyDetail> _requestGoodsSupplyDetails;

    public IReadOnlyList<RequestGoodsSupplyProductHistory> RequestGoodsSupplyProductHistories => _requestGoodsSupplyProductHistories;
    private List<RequestGoodsSupplyProductHistory> _requestGoodsSupplyProductHistories;

    private RequestGoodsSupplyProduct()
    {
        _requestGoodsSupplyManagements = [];
        _requestGoodsSupplyProductHistories = [];
        _requestGoodsSupplyDetails = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
