namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerContractHistory)]
public class EmployerContractHistory : ActivateEntity<EmployerContractHistory, long>
{
    [Description(EContractCmts.IsFirst)]
    public bool IsFirst { get; private set; }
    [Description(EContractCmts.ContractStatus)]
    public EContractStatus Status { get; private set; }
    [Description(GlobalCmts.Code)]
    public string? Code { get; private set; }
    [Description(EContractCmts.CurrencyRate)]
    public decimal? CurrencyRate { get; private set; }
    [Description(GlobalCmts.StartDate)]
    public DateTime? StartDate { get; private set; }
    [Description(GlobalCmts.EndDate)]
    public DateTime? EndDate { get; private set; }
    [Description(EContractCmts.TotalAmount)]
    public decimal TotalAmount { get; private set; } = 0;
    [Description(EContractCmts.AdvancePayment)]
    public decimal AdvancePayment { get; private set; } = 0;
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }
    [Description(GlobalCmts.EmployerContract)]
    public long EmployerContractId { get; private set; }
    public EmployerContract EmployerContract { get; private set; }

    public EmployerContractHistory(
        EmployerContract contract,
        decimal? currencyRate,
        EContractStatus status,
        DateTime? startDate,
        DateTime? endDate,
        decimal advancePayment,
        string? code,
        string? description,
        decimal totalAmount,
        bool isFirst,
        bool isActive) : this()
    {
        SetEmployerContract(contract);
        SetIsFirst(isFirst);
        SetStatus(status);
        SetCode(code);
        SetCurrencyRate(currencyRate);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetTotalAmount(totalAmount);
        SetAdvancePayment(advancePayment);
        SetDescription(description);

        IsActive = isActive;
    }

    public EmployerContractHistory Create(
        EmployerContract contract,
        decimal? currencyRate,
        EContractStatus status,
        DateTime? startDate,
        DateTime? endDate,
        decimal advancePayment,
        string? code,
        string? description,
        decimal totalAmount,
        bool isFirst,
        bool isActive)
    {
        return new EmployerContractHistory(
            contract,
            currencyRate,
            status,
            startDate,
            endDate,
            advancePayment,
            code,
            description,
            totalAmount,
            isFirst,
            isActive);
    }


    #region Methods 
    private void SetEmployerContract(EmployerContract value)
    {
        EmployerContract = Guard.Against.Null(value, nameof(value));
        EmployerContractId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetStatus(EContractStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    private void SetCode(string? value)
    {
        Code = value;
    }

    private void SetCurrencyRate(decimal? value)
    {
        CurrencyRate = value;
    }

    private void SetStartDate(DateTime? value)
    {
        StartDate = value;
    }

    private void SetDescription(string? value)
    {
        Description = value;
    }

    private void SetIsFirst(bool value)
    {
        IsFirst = value;
    }

    private void SetEndDate(DateTime? value)
    {
        EndDate = value;
    }

    private void SetTotalAmount(decimal value)
    {
        TotalAmount = Guard.Against.Null(value, nameof(value));
    }

    private void SetAdvancePayment(decimal value)
    {
        AdvancePayment = Guard.Against.Null(value, nameof(value));
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    private EmployerContractHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
