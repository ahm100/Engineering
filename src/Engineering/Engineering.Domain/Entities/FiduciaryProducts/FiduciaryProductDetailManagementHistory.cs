using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Domain.Entities.FiduciaryProducts;

public class FiduciaryProductDetailManagementHistory : AuditableEntity<FiduciaryProductDetailManagementHistory>
{
    #region Properties

    [Description(FiduciaryProductCmts.InvoiceId)]
    public long InvoiceId { get; private set; }

    [Description(FiduciaryProductCmts.WarehouseId)]
    public long WarehouseId { get; private set; }

    [Description(FiduciaryProductCmts.ConfirmedLoanCount)]
    public int ConfirmedLoanCount { get; private set; }

    [Description(FiduciaryProductCmts.Status)]
    public FiduciaryProductDetailManagementStatus Status { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(FiduciaryProductCmts.FiduciaryProductDetailManagement)]
    public long FiduciaryProductDetailManagementId { get; private set; }
    public FiduciaryProductDetailManagement FiduciaryProductDetailManagement { get; private set; }

    #endregion

    public FiduciaryProductDetailManagementHistory(long invoiceId,
        long warehouseId,
        int confirmedLoanCount,
        FiduciaryProductDetailManagementStatus status,
        FiduciaryProductDetailManagement fiduciaryProductDetailManagement,
        string? description) : this()
    {
        InvoiceId = Guard.Against.Null(invoiceId, nameof(invoiceId));
        WarehouseId = Guard.Against.Null(warehouseId, nameof(warehouseId));
        ConfirmedLoanCount = Guard.Against.Null(confirmedLoanCount, nameof(confirmedLoanCount));
        FiduciaryProductDetailManagement = Guard.Against.Null(fiduciaryProductDetailManagement, nameof(fiduciaryProductDetailManagement));
        Status = Guard.Against.EnumOutOfRange(status, nameof(status));
        Description = description;
    }

    public FiduciaryProductDetailManagementHistory(long invoiceId,
        long warehouseId,
        int confirmedLoanCount,
        FiduciaryProductDetailManagementStatus status,
        FiduciaryProductDetailManagement fiduciaryProductDetailManagement,
        long? userId,
        string? description) : this()
    {
        SetInvoiceId(invoiceId);
        SetWarehouseId(warehouseId);
        SetConfirmedLoanCount(confirmedLoanCount);
        SetFiduciaryProductDetailManagement(fiduciaryProductDetailManagement);
        SetStatus(status);
        CreatorId = userId ?? 0;
        this.Created = DateTime.Now;
        CheckUser = true;
        Description = description;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetInvoiceId(long value)
    {
        InvoiceId = Guard.Against.Null(value, nameof(value));
    }

    public void SetWarehouseId(long value)
    {
        WarehouseId = Guard.Against.Null(value, nameof(value));
    }

    public void SetConfirmedLoanCount(int value)
    {
        ConfirmedLoanCount = Guard.Against.Null(value, nameof(value));
    }

    public void SetStatus(FiduciaryProductDetailManagementStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public void SetFiduciaryProductDetailManagement(FiduciaryProductDetailManagement value)
    {
        FiduciaryProductDetailManagement = Guard.Against.Null(value, nameof(value));
        FiduciaryProductDetailManagementId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private FiduciaryProductDetailManagementHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
