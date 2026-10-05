using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

/// <summary>
/// تاریخچه مدیریت درخواست تامین کالا
/// </summary>
public class RequestGoodsSupplyManagementHistory : AuditableEntity<RequestGoodsSupplyManagementHistory>
{
    [Description(RGSCmts.WarehouseId)]
    public long? WarehouseId { get; private set; }

    [Description(RGSCmts.InvoiceId)]
    public long? InvoiceId { get; private set; }

    [Description(RGSCmts.RequestedCount)]
    public decimal RequestedCount { get; private set; }

    [Description(RGSCmts.AlternateId)]
    public long? AlternateId { get; private set; }

    [Description(RGSCmts.Type)]
    public GoodsSupplyManagementType Type { get; private set; }

    [Description(RGSCmts.Status)]
    public GoodsSupplyManagementStatus Status { get; private set; }

    [Description(RGSCmts.Description)]
    public string? Description { get; private set; }

    [Description(RGSCmts.AssignmentDate)]
    public DateTime? AssignmentDate { get; private set; }

    [Description(RGSCmts.RequestGoodsSupplyManagement)]
    public long RequestGoodsSupplyManagementId { get; private set; }
    public RequestGoodsSupplyManagement RequestGoodsSupplyManagement { get; private set; }

    public RequestGoodsSupplyManagementHistory(RequestGoodsSupplyManagement requestGoodsSupplyManagement,
        long? warehouseId,
        long? invoiceId,
        decimal requestedCount,
        long? alternateId,
        GoodsSupplyManagementType type,
        GoodsSupplyManagementStatus status,
        string? description) : this()
    {
        SetRequestGoodsSupplyManagement(requestGoodsSupplyManagement);
        SetWarehouseId(warehouseId);
        SetInvoiceId(invoiceId);
        SetDescription(description);
        SetRequestedCount(requestedCount);
        SetAlternateId(alternateId);
        SetType(type);
        SetStatus(status);
    }

    public RequestGoodsSupplyManagementHistory(RequestGoodsSupplyManagement requestGoodsSupplyManagement,
        long? warehouseId,
        long? invoiceId,
        decimal requestedCount,
        long? alternateId,
        GoodsSupplyManagementType type,
        GoodsSupplyManagementStatus status,
        string? description,
        long? userId) : this()
    {
        SetRequestGoodsSupplyManagement(requestGoodsSupplyManagement);
        SetWarehouseId(warehouseId);
        SetInvoiceId(invoiceId);
        SetDescription(description);
        SetRequestedCount(requestedCount);
        SetAlternateId(alternateId);
        SetType(type);
        SetStatus(status);
        CreatorId = userId ?? 0;
        CheckUser = true;
        Created = DateTime.UtcNow;
    }

    public void SetWarehouseId(long? value)
    {
        WarehouseId = value;
    }

    public void SetInvoiceId(long? value)
    {
        InvoiceId = value;
    }

    public void SetRequestedCount(decimal value)
    {
        RequestedCount = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public void SetAlternateId(long? value)
    {
        AlternateId = value;
    }

    public void SetType(GoodsSupplyManagementType value)
    {
        Type = Guard.Against.EnumOutOfRange(value, nameof(value));
    }

    public void SetStatus(GoodsSupplyManagementStatus value)
    {
        Status = Guard.Against.EnumOutOfRange(value, nameof(value));
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetAssignmentDate(DateTime? value)
    {
        AssignmentDate = value;
    }

    public void SetRequestGoodsSupplyManagement(
        RequestGoodsSupplyManagement requestGoodsSupplyManagement)
    {
        RequestGoodsSupplyManagement = Guard.Against.Null(requestGoodsSupplyManagement, nameof(requestGoodsSupplyManagement));
        RequestGoodsSupplyManagementId = Guard.Against.Null(requestGoodsSupplyManagement.Id, nameof(requestGoodsSupplyManagement.Id));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public RequestGoodsSupplyManagementHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
