using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Domain.Entities.FiduciaryProducts;

/// <summary>
/// تاریخچه تغییرات کالا های امانی
/// </summary>
public class FiduciaryProductDetailHistory : AuditableEntity<FiduciaryProductDetailHistory>
{
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
    public FiduciaryProductDetailStatus Status { get; private set; }

    [Description(FiduciaryProductCmts.DeliverDate)]
    public DateTime? DeliverDate { get; private set; }

    [Description(FiduciaryProductCmts.FiduciaryProductDetail)]
    public FiduciaryProductDetail FiduciaryProductDetail { get; private set; }

    #endregion

    public FiduciaryProductDetailHistory(long productId,
    int loanCount,
    int loanDays,
    long measureUnitId,
    long currencyId,
    decimal dailyLateFine,
    string? description,
    FiduciaryProductDetailStatus status,
    int? confirmedLoanDays,
    decimal? confirmedDailyLateFine,
    DateTime? deliverDate,
    string? statusDescription,
    string? lastDescription) : this()
    {
        SetProductId(productId);
        SetLoanCount(loanCount);
        SetLoanDays(loanDays);
        SetMeasureUnitId(measureUnitId);
        SetCurrencyId(currencyId);
        SetDailyLateFine(dailyLateFine);
        SetDescription(description);
        SetStatus(status);
        SetConfirmedLoanDays(confirmedLoanDays);
        SetConfirmedDailyLateFine(confirmedDailyLateFine);
        SetDeliverDate(deliverDate);
        SetStatusDescription(statusDescription);
        SetLastDescription(lastDescription);
    }

    public FiduciaryProductDetailHistory(long productId,
        int loanCount,
        int loanDays,
        long measureUnitId,
        long currencyId,
        decimal dailyLateFine,
        string? description,
        FiduciaryProductDetailStatus status,
        int? confirmedLoanDays,
        decimal? confirmedDailyLateFine,
        DateTime? deliverDate,
        long? userId,
        string? statusDescription,
        string? lastDescription) : this()
    {
        SetProductId(productId);
        SetLoanCount(loanCount);
        SetLoanDays(loanDays);
        SetMeasureUnitId(measureUnitId);
        SetCurrencyId(currencyId);
        SetDailyLateFine(dailyLateFine);
        SetDescription(description);
        SetStatus(status);
        SetConfirmedLoanDays(confirmedLoanDays);
        SetConfirmedDailyLateFine(confirmedDailyLateFine);
        SetDeliverDate(deliverDate);
        CreatorId = userId ?? 0;
        CheckUser = true;
        this.Created = DateTime.Now;
        SetStatusDescription(statusDescription);
        SetLastDescription(lastDescription);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetProductId(long value)
    {
        ProductId = Guard.Against.Null(value, nameof(value));
    }

    public void SetLoanCount(int value)
    {
        LoanCount = Guard.Against.Null(value, nameof(value));
    }

    public void SetStatus(FiduciaryProductDetailStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public void SetMeasureUnitId(long value)
    {
        MeasureUnitId = Guard.Against.Null(value, nameof(value));
    }

    public void SetCurrencyId(long value)
    {
        CurrencyId = Guard.Against.Null(value, nameof(value));
    }

    public void SetDailyLateFine(decimal value)
    {
        DailyLateFine = Guard.Against.Null(value, nameof(value));
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

    public void SetDeliverDate(DateTime? deliverDate)
    {
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
    private FiduciaryProductDetailHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
