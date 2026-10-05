using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Domain.Entities.RequestContractors;

[Description(RequestContractorCmts.RequestContractorInquiry)]
public class RequestContractorInquiry : AuditableEntity<RequestContractorInquiry>
{
    #region Properties

    [Description(GlobalCmts.ContractorId)]
    public long ContractorId { get; private set; }

    [Description(GlobalCmts.CurrencyId)]
    public long CurrencyId { get; private set; }

    [Description(RequestContractorCmts.Amount)]
    public decimal Amount { get; private set; }

    [Description(RequestContractorCmts.Discount)]
    public decimal? Discount { get; private set; }

    [Description(RequestContractorCmts.Tax)]
    public decimal? Tax { get; private set; }

    [Description(RequestContractorCmts.TotalAmount)]
    public decimal TotalAmount { get; private set; }

    [Description(RequestContractorCmts.Type)]
    public RequestContractorType Type { get; private set; }

    [Description(RequestContractorCmts.FromDate)]
    public DateTime? FromDate { get; private set; }

    [Description(RequestContractorCmts.ToDate)]
    public DateTime? ToDate { get; private set; }

    [Description(RequestContractorCmts.IsConfirmed)]
    public bool IsConfirmed { get; private set; }

    [Description(RequestContractorCmts.ConfirmedUser)]
    public long? ConfirmedUser { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.RequestContractor)]
    public long RequestContractorId { get; private set; }
    public RequestContractor RequestContractor { get; private set; }

    #endregion

    public RequestContractorInquiry(long contractorId,
        long currencyId,
        decimal totalAmount,
        decimal amount,
        decimal? discount,
        decimal? tax,
        RequestContractorType type,
        DateTime? fromDate,
        DateTime? toDate,
        string? description,
        RequestContractor requestContractor) : this()
    {
        SetContractorId(contractorId);
        SetCurrencyId(currencyId);
        SetTotalAmount(totalAmount);
        SetAmount(amount);
        SetType(type);
        SetDiscount(discount);
        SetTax(tax);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetDescription(description);
        SetRequestContractor(requestContractor);
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private readonly List<RequestContractorInquiryDocument> _documents;
    public IReadOnlyList<RequestContractorInquiryDocument> RequestContractorInquiryDocuments => _documents;

    private RequestContractorInquiry()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _documents = [];
    }

    #endregion

    #region Commands

    public void SetData(
        long contractorId,
        long currencyId,
        decimal totalAmount,
        decimal amount,
        decimal? discount,
        decimal? tax,
        RequestContractorType type,
        DateTime? fromDate,
        DateTime? toDate,
        string? description,
        RequestContractor requestContractor)
    {
        ContractorId = Guard.Against.Null(contractorId, nameof(contractorId));
        CurrencyId = Guard.Against.Null(currencyId, nameof(currencyId));
        TotalAmount = Guard.Against.NegativeOrZero(totalAmount, nameof(totalAmount));
        Amount = Guard.Against.NegativeOrZero(amount, nameof(amount));
        Type = Guard.Against.Null(type, nameof(type));
        Discount = discount;
        Tax = tax;
        FromDate = fromDate;
        ToDate = toDate;
        Description = description;
        RequestContractor = Guard.Against.Null(requestContractor, nameof(requestContractor));
    }
    public void SetConfirmed(long confirmedUser)
    {
        IsConfirmed = true;
        ConfirmedUser = confirmedUser;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetContractorId(long value)
    {
        ContractorId = Guard.Against.Null(value, nameof(value));
    }

    public void SetCurrencyId(long value)
    {
        CurrencyId = Guard.Against.Null(value, nameof(value)); ;
    }

    public void SetAmount(decimal value)
    {
        Amount = Guard.Against.Null(value, nameof(value)); ;
    }

    public void SetTotalAmount(decimal value)
    {
        TotalAmount = Guard.Against.Null(value, nameof(value)); ;
    }

    public void SetType(RequestContractorType value)
    {
        Type = value;
    }

    public void SetFromDate(DateTime? value)
    {
        FromDate = value;
    }

    public void SetDiscount(decimal? value)
    {
        Discount = value;
    }

    public void SetTax(decimal? value)
    {
        Tax = value;
    }

    public void SetToDate(DateTime? value)
    {
        ToDate = value;
    }

    public void SetIsConfirmed(bool value)
    {
        IsConfirmed = value;
    }

    public void SetConfirmedUser(long? value)
    {
        ConfirmedUser = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetRequestContractor(RequestContractor value)
    {
        RequestContractor = value;
    }
    #endregion
}
