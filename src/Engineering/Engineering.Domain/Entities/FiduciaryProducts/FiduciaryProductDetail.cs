using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Domain.Entities.FiduciaryProducts;

/// <summary>
/// کالای های امانی
/// </summary>
public class FiduciaryProductDetail : AuditableEntity<FiduciaryProductDetail>
{
    #region Fields

    #endregion

    #region Properties

    [Description(GlobalCmts.ProductId)]
    public long ProductId { get; private set; }

    [Description(FiduciaryProductCmts.LoanCount)]
    public int LoanCount { get; private set; }

    [Description(FiduciaryProductCmts.LoanDays)]
    public int LoanDays { get; private set; }

    [Description(FiduciaryProductCmts.ConfirmedLoanDays)]
    public int? ConfirmedLoanDays { get; private set; }

    [Description(FiduciaryProductCmts.MeasureUnitId)]
    public long MeasureUnitId { get; private set; }

    [Description(GlobalCmts.CurrencyId)]
    public long CurrencyId { get; private set; }

    [Description(FiduciaryProductCmts.DailyLateFine)]
    public decimal DailyLateFine { get; private set; }

    [Description(FiduciaryProductCmts.ConfirmedDailyLateFine)]
    public decimal? ConfirmedDailyLateFine { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(FiduciaryProductCmts.StatusDescription)]
    public string? StatusDescription { get; private set; } = string.Empty;

    [Description(FiduciaryProductCmts.LastDescription)]
    public string? LastDescription { get; private set; } = string.Empty;

    [Description(GlobalCmts.Status)]
    public FiduciaryProductDetailStatus Status { get; private set; } = FiduciaryProductDetailStatus.New;

    [Description(FiduciaryProductCmts.DeliverDate)]
    public DateTime? DeliverDate { get; private set; }

    [Description(FiduciaryProductCmts.FiduciaryProduct)]
    public long FiduciaryProductId { get; private set; }
    public FiduciaryProduct FiduciaryProduct { get; private set; }

    #endregion

    public FiduciaryProductDetail(FiduciaryProduct fiduciaryProduct,
        long productId,
        int loanCount,
        int loanDays,
        long measureUnitId,
        long currencyId,
        int dailyLateFine) : this()
    {
        SetFiduciaryProduct(fiduciaryProduct);
        SetProductId(productId);
        SetLoanCount(loanCount);
        SetLoanDays(loanDays);
        SetMeasureUnitId(measureUnitId);
        SetCurrencyId(currencyId);
        SetDailyLateFine(dailyLateFine);

        AddHistory();
    }

    public void AddHistory()
    {
        _histories.Add(new FiduciaryProductDetailHistory(ProductId, LoanCount, LoanDays, MeasureUnitId, CurrencyId, DailyLateFine,
            Description, Status, ConfirmedLoanDays, ConfirmedDailyLateFine, DeliverDate, StatusDescription, LastDescription));
    }

    public void AddHistory(long? userId)
    {
        _histories.Add(new FiduciaryProductDetailHistory(ProductId, LoanCount, LoanDays, MeasureUnitId, CurrencyId, DailyLateFine,
            Description, Status, ConfirmedLoanDays, ConfirmedDailyLateFine, DeliverDate, userId, StatusDescription, LastDescription));
    }

    public void ChangeStatus(FiduciaryProductDetailStatus status)
    {
        ArgumentNullException.ThrowIfNull(status);
        Status = status;
        AddHistory();
    }

    public void ChangeStatusWithUser(FiduciaryProductDetailStatus status, long? userId)
    {
        ArgumentNullException.ThrowIfNull(status);
        Status = status;
        AddHistory(userId);
    }

    public void ChangeStatusWithUser(FiduciaryProductDetailStatus status, string? statusDescription, string? lastDescription, long? userId)
    {
        ArgumentNullException.ThrowIfNull(status);
        StatusDescription = statusDescription;
        LastDescription = lastDescription;
        Status = status;
        AddHistory(userId);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetFiduciaryProduct(FiduciaryProduct value)
    {
        FiduciaryProduct = Guard.Against.Null(value, nameof(value));
        FiduciaryProductId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProductId(long productId)
    {
        ArgumentNullException.ThrowIfNull(productId);
        ProductId = productId;
    }

    public void SetLoanCount(int loanCount)
    {
        ArgumentNullException.ThrowIfNull(loanCount);
        LoanCount = loanCount;
    }

    public void SetMeasureUnitId(long measureUnitId)
    {
        ArgumentNullException.ThrowIfNull(measureUnitId);
        MeasureUnitId = measureUnitId;
    }

    public void SetCurrencyId(long currencyId)
    {
        ArgumentNullException.ThrowIfNull(currencyId);
        CurrencyId = currencyId;
    }

    public void SetDailyLateFine(int dailyLateFine)
    {
        ArgumentNullException.ThrowIfNull(dailyLateFine);
        DailyLateFine = dailyLateFine;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetConfirmedDailyLateFine(decimal? confirmedDailyLateFine)
    {
        ConfirmedDailyLateFine = confirmedDailyLateFine;
    }

    public void SetLoanDays(int loanDays)
    {
        ArgumentNullException.ThrowIfNull(loanDays);
        LoanDays = loanDays;
    }

    public void SetConfirmedLoanDays(int? confirmedLoanDays)
    {
        ConfirmedLoanDays = confirmedLoanDays;
    }

    public void SetDeliverDate(DateTime deliverDate)
    {
        ArgumentNullException.ThrowIfNull(deliverDate);
        DeliverDate = deliverDate;
    }

    public void SetStatusDescription(string? description)
    {
        StatusDescription = description;
    }

    public void SetLastDescription(string? description)
    {
        LastDescription = description;
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<FiduciaryProductDetailHistory> _histories;
    public IReadOnlyList<FiduciaryProductDetailHistory> Histories => _histories;

    private List<FiduciaryProductDetailManagement> _managements;
    public IReadOnlyList<FiduciaryProductDetailManagement> Managements => _managements;

    private FiduciaryProductDetail()
    {
        _histories = [];
        _managements = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
