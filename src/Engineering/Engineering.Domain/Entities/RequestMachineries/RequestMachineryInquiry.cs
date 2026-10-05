using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Domain.Entities.RequestMachineries;

/// <summary>
/// استعلام درخواست ماشین آلات
/// </summary>
public class RequestMachineryInquiry : AuditableEntity<RequestMachineryInquiry>
{

    #region Properties

    [Description(RequestMachineryCmts.ThirdPartyId)]
    public long ThirdPartyId { get; private set; }

    [Description(RequestMachineryCmts.CurrencyId)]
    public long CurrencyId { get; private set; }

    [Description(RequestMachineryCmts.Count)]
    public int Count { get; private set; }

    [Description(RequestMachineryCmts.Unit)]
    public RequestMachineryUnit Unit { get; private set; }

    [Description(RequestMachineryCmts.UnitPrice)]
    public decimal UnitPrice { get; private set; }

    [Description(RequestMachineryCmts.TotalPrice)]
    public decimal TotalPrice { get; private set; }

    [Description(RequestMachineryCmts.IsConfirmed)]
    public bool IsConfirmed { get; private set; }

    [Description(RequestMachineryCmts.ConfirmedUser)]
    public long? ConfirmedUser { get; private set; }

    [Description(RequestMachineryCmts.Description)]
    public string? Description { get; private set; }

    [Description(RequestMachineryCmts.InquiryRequestedTime)]
    public decimal InquiryRequestedTime { get; private set; }

    [Description(RequestMachineryCmts.RequestMachineryInquiryOperator)]
    public long RequestMachineryInquiryOperatorId { get; private set; }
    public RequestMachineryInquiryOperator RequestMachineryInquiryOperator { get; private set; }

    #endregion

    #region Constructors


    public RequestMachineryInquiry(long thirdPartyId,
        long currencyId,
        int count,
        RequestMachineryUnit unit,
        decimal unitPrice,
        decimal totalPrice,
        string? description,
        decimal inquiryRequestedTime,
        RequestMachineryInquiryOperator requestMachineryInquiryOperator) : this()
    {
        SetThirdPartyId(thirdPartyId);
        SetCurrencyId(currencyId);
        SetCount(count);
        SetUnit(unit);
        SetUnitPrice(unitPrice);
        SetTotalPrice(totalPrice);
        SetInquiryRequestedTime(inquiryRequestedTime);
        SetDescription(description);
        SetRequestMachineryInquiryOperator(requestMachineryInquiryOperator);
    }

    #endregion

    #region Commands

    public void SetData(long thirdPartyId,
        long currencyId,
        int count,
        RequestMachineryUnit unit,
        decimal unitPrice,
        decimal totalPrice,
        string? description,
        decimal inquiryRequestedTime,
        RequestMachineryInquiryOperator requestMachineryInquiryOperator)
    {
        SetThirdPartyId(thirdPartyId);
        SetCurrencyId(currencyId);
        SetCount(count);
        SetUnit(unit);
        SetUnitPrice(unitPrice);
        SetTotalPrice(totalPrice);
        SetInquiryRequestedTime(inquiryRequestedTime);
        SetDescription(description);
        SetRequestMachineryInquiryOperator(requestMachineryInquiryOperator);
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

    public void SetThirdPartyId(long thirdPartyId)
    {
        ArgumentNullException.ThrowIfNull(thirdPartyId);

        ThirdPartyId = thirdPartyId;
    }

    public void SetRequestMachineryInquiryOperator(RequestMachineryInquiryOperator value)
    {
        RequestMachineryInquiryOperator = Guard.Against.Null(value, nameof(value));
        RequestMachineryInquiryOperatorId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetCount(int count)
    {
        ArgumentNullException.ThrowIfNull(count);

        Count = count;
    }

    public void SetInquiryRequestedTime(decimal inquiryRequestedTime)
    {
        ArgumentNullException.ThrowIfNull(inquiryRequestedTime);

        InquiryRequestedTime = inquiryRequestedTime;
    }

    public void SetUnit(RequestMachineryUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);

        Unit = unit;
    }

    public void SetUnitPrice(decimal unitPrice)
    {
        ArgumentNullException.ThrowIfNull(unitPrice);

        UnitPrice = unitPrice;
    }

    public void SetTotalPrice(decimal totalPrice)
    {
        ArgumentNullException.ThrowIfNull(totalPrice);

        TotalPrice = totalPrice;
    }

    public void SetCurrencyId(long currencyId)
    {
        ArgumentNullException.ThrowIfNull(currencyId);

        CurrencyId = currencyId;
    }
    public void SetDescription(string? desctiption)
    {
        Description = desctiption;
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public List<RequestMachineryInquiryDocument> RequestMachineryInquiryDocuments { get; private set; } = null!;
    private RequestMachineryInquiry()
    {
        RequestMachineryInquiryDocuments = [];
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
