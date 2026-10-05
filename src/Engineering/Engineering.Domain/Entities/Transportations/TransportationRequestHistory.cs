using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Domain.Entities.Transportations;

[Description(GlobalCmts.Histories)]
public class TransportationRequestHistory : AuditableEntity<TransportationRequestHistory>
{

    #region Cals

    [Description(TransportationCmts.StartDate)]
    public DateTime? StartDate { get; private set; }

    [Description(TransportationCmts.EndDate)]
    public DateTime? EndDate { get; private set; }

    [Description(TransportationCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(TransportationCmts.DriverId)]
    public long? DriverId { get; private set; }

    [Description(TransportationCmts.DriverName)]
    public string? DriverName { get; private set; }

    [Description(TransportationCmts.AccountNumber)]
    public string? AccountNumber { get; private set; }

    [Description(TransportationCmts.BankId)]
    public long? BankId { get; private set; }

    [Description(TransportationCmts.CardNumber)]
    public string? CardNumber { get; private set; }

    [Description(TransportationCmts.AccountName)]
    public string? AccountName { get; private set; }

    [Description(TransportationCmts.IBAN)]
    public string? IBAN { get; private set; }

    [Description(TransportationCmts.Price)]
    public decimal? Price { get; private set; }

    [Description(TransportationCmts.CurrencyUnitId)]
    public long? CurrencyUnitId { get; private set; }

    [Description(TransportationCmts.AccountDescription)]
    public string? AccountDescription { get; private set; } = string.Empty;

    [Description(TransportationCmts.TransportationRequestStatus)]
    public TransportationRequestStatus? TransportationRequestStatus { get; private set; }

    [Description(TransportationCmts.TransportationPaymentType)]
    public TransportationPaymentType? TransportationPaymentType { get; private set; }

    [Description(TransportationCmts.ManagerDescription)]
    public string? ManagerDescription { get; private set; } = string.Empty;

    [Description(TransportationCmts.ConfirmUserId)]
    public long? ConfirmUserId { get; private set; }

    [Description(TransportationCmts.ConfirmDate)]
    public DateTime? ConfirmDate { get; private set; }

    [Description(TransportationCmts.PaymentOrderId)]
    public long? PaymentOrderId { get; private set; }

    [Description(TransportationCmts.PaymentDate)]
    public DateTime? PaymentDate { get; private set; }

    [Description(TransportationCmts.TransportationRequest)]
    public long TransportationRequestId { get; set; }
    public TransportationRequest TransportationRequest { get; set; }

    #endregion

    #region TransportationRequestHistory Create
    public TransportationRequestHistory(
        DateTime? startDate,
        DateTime? endDate,
        string? description,
        long? driverId,
        string? driverName,
        string? accountNumber,
        long? bankId,
        string? cardNumber,
        string? accountName,
        string? iBAN,
        decimal? price,
        long? currencyUnitId,
        string? accountDescription,
        TransportationPaymentType? transportationPaymentType,
        TransportationRequestStatus? transportationRequestStatus,
        string? managerDescription,
        long? confirmUserId,
        DateTime? confirmDate,
        long? paymentOrderId,
        DateTime? paymentDate,
        TransportationRequest transportationRequest
        )
    {
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetDescription(description);
        SetDriverId(driverId);
        SetDriverName(driverName);
        SetAccountNumber(accountNumber);
        SetBankId(bankId);
        SetCardNumber(cardNumber);
        SetAccountName(accountName);
        SetIBAN(iBAN);
        SetPrice(price);
        SetCurrencyUnitId(currencyUnitId);
        SetAccountDescription(accountDescription);
        SetTransportationPaymentType(transportationPaymentType);
        SetTransportationRequestStatus(transportationRequestStatus);
        SetTransportationRequest(transportationRequest);
        SetManagerDescription(managerDescription);
        SetConfirmUserId(confirmUserId);
        SetConfirmDate(confirmDate);
        SetPaymentOrderId(paymentOrderId);
        SetPaymentDate(paymentDate);
    }
    #endregion

    public void SetTransportationRequest(TransportationRequest value)
    {
        TransportationRequest = Guard.Against.Null(value, nameof(value));
        TransportationRequestId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetPaymentOrderId(long? value)
    {
        PaymentOrderId = value;
    }
    public void SetPaymentDate(DateTime? value)
    {
        PaymentDate = value;
    }
    public void SetTransportationPaymentType(TransportationPaymentType? value)
    {
        TransportationPaymentType = value;
    }
    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }
    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
    }
    public void SetDriverId(long? value)
    {
        DriverId = value;
    }
    public void SetDriverName(string? value)
    {
        DriverName = value;
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetAccountNumber(string? value)
    {
        AccountNumber = value;
    }
    public void SetBankId(long? value)
    {
        BankId = value;
    }
    public void SetCardNumber(string? value)
    {
        CardNumber = value;
    }
    public void SetAccountName(string? value)
    {
        AccountName = value;
    }
    public void SetIBAN(string? value)
    {
        IBAN = value;
    }
    public void SetPrice(decimal? value)
    {
        Price = value;
    }
    public void SetCurrencyUnitId(long? value)
    {
        CurrencyUnitId = value;
    }
    public void SetAccountDescription(string? value)
    {
        AccountDescription = value;
    }
    public void SetManagerDescription(string? value)
    {
        ManagerDescription = value;
    }
    public void SetConfirmUserId(long? value)
    {
        ConfirmUserId = value;
    }
    public void SetConfirmDate(DateTime? value)
    {
        ConfirmDate = value;
    }
    public void SetFareAmount(decimal? value)
    {
        Price = value;
    }
    public void SetTransportationRequestStatus(TransportationRequestStatus? value)
    {
        TransportationRequestStatus = value;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetStartDate(DateTime? value)
    {
        StartDate = value;
    }
    public void SetEndDate(DateTime? value)
    {
        EndDate = value;
    }
    public void SetTransportationRequestHistoryStatus(TransportationRequestStatus? value)
    {
        TransportationRequestStatus = Guard.Against.Null(value, nameof(value));
    }


    /// <summary>
    ///  For EF core, never touch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationRequestHistory()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
