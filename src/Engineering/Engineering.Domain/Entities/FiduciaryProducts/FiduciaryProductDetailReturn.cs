using Engineering.Domain.Entities.FiduciaryProducts.Enums;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Domain.Entities.FiduciaryProducts;

[Description(FiduciaryProductCmts.FiduciaryProductDetailReturn)]
public class FiduciaryProductDetailReturn : AuditableEntity<FiduciaryProductDetailReturn>
{
    #region Properties

    [Description(FiduciaryProductCmts.InvoiceId)]
    public long InvoiceId { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(FiduciaryProductCmts.ReturnCount)]
    public int ReturnCount { get; private set; }

    [Description(FiduciaryProductCmts.ReturnDate)]
    public DateTime ReturnDate { get; private set; }

    [Description(FiduciaryProductCmts.LateDay)]
    public int LateDay { get; private set; }

    [Description(FiduciaryProductCmts.LateFine)]
    public decimal LateFine { get; private set; }

    [Description(GlobalCmts.CurrencyId)]
    public long CurrencyId { get; private set; }

    [Description(FiduciaryProductCmts.Type)]
    public FiduciaryProductDetailReturnType Type { get; private set; }

    [Description(FiduciaryProductCmts.FiduciaryProductDetailManagement)]
    public long FiduciaryProductDetailManagementId { get; private set; }
    public FiduciaryProductDetailManagement FiduciaryProductDetailManagement { get; private set; }

    #endregion

    public FiduciaryProductDetailReturn(long invoiceId,
        string? description,
        int returnCount,
        DateTime returnDate,
        int lateDay,
        decimal lateFine,
        long currencyId,
        FiduciaryProductDetailReturnType type,
        FiduciaryProductDetailManagement fiduciaryProductDetailManagement) : this()
    {
        SetDescription(description);
        SetReturnCount(returnCount);
        SetReturnDate(returnDate);
        SetLateFine(lateFine);
        SetLateDay(lateDay);
        SetCurrencyId(currencyId);
        SetType(type);
        SetInvoiceId(invoiceId);
        SetFiduciaryProductDetailManagement(fiduciaryProductDetailManagement);
    }

    public void SetInvoiceId(long value)
    {
        InvoiceId = Guard.Against.Null(value, nameof(value));
    }

    public void SetFiduciaryProductDetailManagement(FiduciaryProductDetailManagement value)
    {
        FiduciaryProductDetailManagement = Guard.Against.Null(value, nameof(value));
        FiduciaryProductDetailManagementId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetReturnCount(int returnCount)
    {
        ArgumentNullException.ThrowIfNull(returnCount);
        ReturnCount = returnCount;
    }

    public void SetReturnDate(DateTime returnDate)
    {
        ArgumentNullException.ThrowIfNull(returnDate);
        ReturnDate = returnDate;
    }

    public void SetLateDay(int lateDay)
    {
        ArgumentNullException.ThrowIfNull(lateDay);
        LateDay = lateDay;
    }

    public void SetLateFine(decimal lateFine)
    {
        ArgumentNullException.ThrowIfNull(lateFine);
        LateFine = lateFine;
    }

    public void SetCurrencyId(long currencyId)
    {
        ArgumentNullException.ThrowIfNull(currencyId);
        CurrencyId = currencyId;
    }

    public void SetType(FiduciaryProductDetailReturnType type)
    {
        ArgumentNullException.ThrowIfNull(type);
        Type = type;
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<FiduciaryProductDetailReturnDocument> _documents;
    public IReadOnlyList<FiduciaryProductDetailReturnDocument> Documents => _documents;

    private List<RequestReward> _requestRewards;
    public IReadOnlyList<RequestReward> RequestRewards => _requestRewards;
    private FiduciaryProductDetailReturn()
    {
        _documents = [];
        _requestRewards = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}
