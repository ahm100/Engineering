using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Domain.Entities.FiduciaryProducts;


/// <summary>
/// تاریخچه تعداد تاییدی از انبار کالای امانی
/// </summary>
public class FiduciaryProductDetailManagement : AuditableEntity<FiduciaryProductDetailManagement>
{

    #region Properties

    [Description(FiduciaryProductCmts.InvoiceId)]
    public long InvoiceId { get; private set; }

    [Description(FiduciaryProductCmts.WarehouseId)]
    public long WarehouseId { get; private set; }

    [Description(FiduciaryProductCmts.DestinationWarehouseId)]
    public long? DestinationWarehouseId { get; private set; }

    [Description(FiduciaryProductCmts.ConfirmedLoanCount)]
    public int ConfirmedLoanCount { get; private set; }

    [Description(FiduciaryProductCmts.Status)]
    public FiduciaryProductDetailManagementStatus Status { get; private set; } = FiduciaryProductDetailManagementStatus.Pending;

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(FiduciaryProductCmts.FiduciaryProductDetailId)]
    public long FiduciaryProductDetailId { get; private set; }
    public FiduciaryProductDetail FiduciaryProductDetail { get; private set; }
    #endregion

    public FiduciaryProductDetailManagement(
        long invoiceId,
        long warehouseId,
        long? destWarehouseId,
        int confirmedLoanCount,
        string? description,
        FiduciaryProductDetail fiduciaryProductDetail) : this()
    {
        SetInvoiceId(invoiceId);
        SetWarehouseId(warehouseId);
        SetConfirmedLoanCount(confirmedLoanCount);
        SetFiduciaryProductDetail(fiduciaryProductDetail);
        SetDescription(description);
        SetDestWarehouseId(destWarehouseId);

        AddHistory();
    }

    public void AddHistory()
    {
        _histories.Add(new FiduciaryProductDetailManagementHistory(InvoiceId, WarehouseId, ConfirmedLoanCount, Status, this, Description));
    }

    public void AddHistory(long? userId)
    {
        _histories.Add(new FiduciaryProductDetailManagementHistory(InvoiceId, WarehouseId, ConfirmedLoanCount, Status, this, userId, Description));
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetInvoiceId(long value)
    {
        InvoiceId = Guard.Against.Null(value, nameof(value));
    }

    public void SetFiduciaryProductDetail(FiduciaryProductDetail value)
    {
        FiduciaryProductDetail = Guard.Against.Null(value, nameof(value));
        FiduciaryProductDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetWarehouseId(long value)
    {
        WarehouseId = Guard.Against.Null(value, nameof(value));
    }

    public void SetConfirmedLoanCount(int value)
    {
        ConfirmedLoanCount = Guard.Against.Null(value, nameof(value));
    }

    public void SetDestWarehouseId(long? value)
    {
        DestinationWarehouseId = value;
    }

    public void SetApproved(string? value, long? userId)
    {
        Status = FiduciaryProductDetailManagementStatus.Approved;
        SetDescription(value);
        AddHistory(userId);
    }

    public void SetRejected(string? value, long? userId)
    {
        Status = FiduciaryProductDetailManagementStatus.Rejected;
        SetDescription(value);
        AddHistory(userId);
    }

    public void SetPending(string? value, long? userId)
    {
        Status = FiduciaryProductDetailManagementStatus.Pending;
        SetDescription(value);
        AddHistory(userId);
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<FiduciaryProductDetailManagementHistory> _histories;
    public IReadOnlyList<FiduciaryProductDetailManagementHistory> Histories => _histories;

    private List<FiduciaryProductDetailReturn> _returns;
    public IReadOnlyList<FiduciaryProductDetailReturn> Returns => _returns;
    private FiduciaryProductDetailManagement()
    {
        _histories = [];
        _returns = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
