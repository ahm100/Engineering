using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Domain.Entities.RequestGoodsSupplies;

[Description(RGSCmts.RequestGoodsSupplyManagement)]
public class RequestGoodsSupplyManagement : AuditableEntity<RequestGoodsSupplyManagement>
{
    [Description(RGSCmts.ReferenceId)]
    public long? ReferenceId { get; private set; }

    [Description(RGSCmts.WarehouseId)]
    public long? WarehouseId { get; private set; }

    [Description(RGSCmts.DestinationWarehouseId)]
    public long? DestinationWarehouseId { get; private set; }

    [Description(RGSCmts.InvoiceId)]
    public long? InvoiceId { get; private set; }

    [Description(RGSCmts.RequestedCount)]
    public decimal RequestedCount { get; private set; }

    [Description(RGSCmts.ConfirmedRequestCount)]
    public decimal? ConfirmedRequestCount { get; private set; }

    [Description(RGSCmts.AlternateId)]
    public long? AlternateId { get; private set; }

    [Description(RGSCmts.Description)]
    public string? Description { get; private set; }

    [Description(RGSCmts.Type)]
    public GoodsSupplyManagementType Type { get; private set; }

    [Description(RGSCmts.Status)]
    public GoodsSupplyManagementStatus Status { get; private set; } = GoodsSupplyManagementStatus.Pending;

    [Description(RGSCmts.AssignmentDate)]
    public DateTime? AssignmentDate { get; private set; }

    [Description(RGSCmts.OperatorAppointmentId)]
    public long? OperatorAppointmentId { get; private set; }

    [Description(RGSCmts.LastDescription)]
    public string? LastDescription { get; private set; }

    [Description(RGSCmts.RequestGoodsSupplyDetail)]
    public long? RequestGoodsSupplyDetailId { get; private set; }
    public RequestGoodsSupplyDetail? RequestGoodsSupplyDetail { get; private set; }

    [Description(RGSCmts.RequestGoodsSupplyProduct)]
    public long? RequestGoodsSupplyProductId { get; private set; }
    public RequestGoodsSupplyProduct? RequestGoodsSupplyProduct { get; private set; }

    [Description(RGSCmts.RequestGoodsSupplyType)]
    public long? RequestGoodsSupplyTypeId { get; private set; }
    public RequestGoodsSupplyType? RequestGoodsSupplyType { get; private set; }

    public RequestGoodsSupplyManagement(RequestGoodsSupplyProduct requestGoodsSupplyDetail, long? referenceId, long? warehouseId, long? destinationWarehouseId, long? invoiceId,
        decimal requestedCount, long? alternateId, GoodsSupplyManagementType type, string? description, string? lastDescription) : this()
    {
        SetRequestGoodsSupplyProduct(requestGoodsSupplyDetail);
        SetReferenceId(referenceId);
        SetWarehouseId(warehouseId);
        SetDestinationWarehouseId(destinationWarehouseId);
        SetInvoiceId(invoiceId);
        SetRequestedCount(requestedCount);
        SetAlternateId(alternateId);
        SetDescription(description);
        SetType(type);
        SetLastDescription(lastDescription);

        AddHistory();
    }

    public static RequestGoodsSupplyManagement Create(RequestGoodsSupplyProduct requestGoodsSupplyDetail, long? productId, long? warehouseId, long? destinationWarehouseId,
        long? invoiceId, decimal requestedCount, long? alternateId, GoodsSupplyManagementType type, string? description, string? lastDescription)
    {
        return new RequestGoodsSupplyManagement(requestGoodsSupplyDetail, productId, warehouseId, destinationWarehouseId, invoiceId, requestedCount, alternateId, type, description, lastDescription);
    }

    public void StatusChanger(GoodsSupplyManagementStatus status, string? description, long? userId, long? operatorAppointmentId, decimal? confirmedCount)
    {
        switch (status)
        {
            case GoodsSupplyManagementStatus.CompleteSupply:
                Status = status;
                AssignmentDate = DateTime.Now;
                if (confirmedCount is not null && confirmedCount > 0)
                {
                    if (confirmedCount < RequestedCount)
                    {
                        Status = GoodsSupplyManagementStatus.InCompleteSupply;
                        ConfirmedRequestCount = confirmedCount;
                    }
                    else
                        ConfirmedRequestCount = RequestedCount;
                }
                SetOperatorAppointmentId(operatorAppointmentId);
                break;
            case GoodsSupplyManagementStatus.InCompleteSupply:
                Status = status;
                AssignmentDate = DateTime.Now;
                SetOperatorAppointmentId(operatorAppointmentId);
                break;
            case GoodsSupplyManagementStatus.CommercialInvoiceConfirmation:
                Status = status;
                AssignmentDate = DateTime.Now;
                SetOperatorAppointmentId(operatorAppointmentId);
                break;
            case GoodsSupplyManagementStatus.Return:
                Status = status;
                AssignmentDate = DateTime.Now;
                SetOperatorAppointmentId(operatorAppointmentId);
                break;
            case GoodsSupplyManagementStatus.ReturnToSupply:
                Status = status;
                AssignmentDate = DateTime.Now;
                SetOperatorAppointmentId(operatorAppointmentId);
                break;
            case GoodsSupplyManagementStatus.Pending:
                Status = status;
                AssignmentDate = DateTime.Now;
                SetOperatorAppointmentId(operatorAppointmentId);
                break;
            case GoodsSupplyManagementStatus.PendingForConfirme:
                Status = status;
                AssignmentDate = DateTime.Now;
                SetOperatorAppointmentId(operatorAppointmentId);
                break;
        }

        AddHistory(userId, description);
        SetLastDescription(description);
    }

    public void SetLastDescription(string? value)
    {
        LastDescription = value;
    }

    public void SetAlternateId(long? alternateId)
    {
        AlternateId = alternateId;
    }

    public void SetReferenceId(long? value)
    {
        ReferenceId = value;
    }

    public void SetOperatorAppointmentId(long? value)
    {
        OperatorAppointmentId = value;
    }

    public void SetWarehouseId(long? value)
    {
        WarehouseId = value;
    }

    public void SetRequestGoodsSupplyDetail(
    RequestGoodsSupplyDetail? requestGoodsSupplyDetail)
    {
        RequestGoodsSupplyDetail = requestGoodsSupplyDetail;
    }

    public void SetRequestGoodsSupplyProduct(
    RequestGoodsSupplyProduct requestGoodsSupplyProduct)
    {
        RequestGoodsSupplyProduct = Guard.Against.Null(requestGoodsSupplyProduct, nameof(requestGoodsSupplyProduct));
    }

    public void SetDestinationWarehouseId(long? value)
    {
        DestinationWarehouseId = value;
    }

    public void SetInvoiceId(long? value)
    {
        InvoiceId = value;
    }

    public void SetRequestedCount(decimal value)
    {
        RequestedCount = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public void SetType(GoodsSupplyManagementType value)
    {
        Type = Guard.Against.EnumOutOfRange(value, nameof(value));
    }

    public void SetPendingForConfirme()
    {
        Status = GoodsSupplyManagementStatus.PendingForConfirme;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void AddHistory()
    {
        _histories.Add(new RequestGoodsSupplyManagementHistory(this, WarehouseId, InvoiceId, RequestedCount, AlternateId, Type, Status, Description));
    }

    public void AddHistory(long? userId, string? description)
    {
        _histories.Add(new RequestGoodsSupplyManagementHistory(this, WarehouseId, InvoiceId, RequestedCount, AlternateId, Type, Status, description, userId));
    }

    public void Update(long? warehouseId, long? invoiceId, decimal requestedCount, GoodsSupplyManagementType type)
    {
        WarehouseId = warehouseId;
        InvoiceId = invoiceId;
        RequestedCount = Guard.Against.NegativeOrZero(requestedCount, nameof(requestedCount));
        Type = Guard.Against.EnumOutOfRange(type, nameof(type));
        Status = GoodsSupplyManagementStatus.PendingForConfirme;

        AddHistory();
    }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<RequestGoodsSupplyManagementHistory> _histories;
    public IReadOnlyList<RequestGoodsSupplyManagementHistory> Histories => _histories;
    public RequestGoodsSupplyManagement()
    {
        _histories = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
