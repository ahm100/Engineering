using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractLegalSnapshot)]
public class ContractLegalSnapshot : AuditableEntity<ContractLegalSnapshot, long>
{
    public long ContractId { get; private set; }
    public Contract Contract { get; private set; } = null!;

    [Description(GlobalCmts.ProjectId)]
    public long ProjectId { get; private set; }

    [Description(ContractCmts.ContractPartyId)]
    public long ContractPartyId { get; private set; }

    [Description(GlobalCmts.FaTitle)]
    public string FaTitle { get; private set; } = string.Empty;

    [Description(GlobalCmts.EnTitle)]
    public string? EnTitle { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(ContractCmts.Duration)]
    public int Duration { get; private set; }

    [Description(ContractCmts.DurationUnit)]
    public ContractDurationUnit DurationUnit { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }

    [Description(GlobalCmts.Status)]
    public ContractStatus Status { get; private set; }

    [Description(GlobalCmts.CurrencyId)]
    public long? CurrencyId { get; private set; }

    [Description(ContractCmts.InitialAmount)]
    public decimal InitialAmount { get; private set; }

    [Description(ContractCmts.FinalContractAmount)]
    public decimal FinalAmount { get; private set; }

    private ContractLegalSnapshot()
    {
    }

    public ContractLegalSnapshot(
        Contract contract,
        ContractStatus status,
        decimal initialAmount,
        decimal finalAmount,
        long? currencyId)
    {
        SetContract(contract);
        SetHeader(contract);
        SetStatus(status);
        SetCurrencyId(currencyId);
        SetAmounts(initialAmount, finalAmount);
    }

    private void SetContract(Contract value)
    {
        Contract = Guard.Against.Null(value, nameof(value));
        ContractId = value.Id;
    }

    private void SetHeader(Contract contract)
    {
        ProjectId = Guard.Against.NegativeOrZero(contract.ProjectId, nameof(contract.ProjectId));
        ContractPartyId = Guard.Against.NegativeOrZero(
            contract.ContractPartyId,
            nameof(contract.ContractPartyId));
        FaTitle = Guard.Against.NullOrWhiteSpace(contract.FaTitle, nameof(contract.FaTitle));
        EnTitle = contract.EnTitle;
        Description = contract.Description;
        StartDate = contract.StartDate;
        Duration = Guard.Against.NegativeOrZero(contract.Duration, nameof(contract.Duration));
        DurationUnit = Guard.Against.EnumOutOfRange(
            contract.DurationUnit,
            nameof(contract.DurationUnit));
        EndDate = contract.EndDate;
    }

    private void SetStatus(ContractStatus value)
    {
        Status = Guard.Against.EnumOutOfRange(value, nameof(value));

        if (Status is not (ContractStatus.Active
                or ContractStatus.Suspended
                or ContractStatus.Finished
                or ContractStatus.Terminated))
            throw new InvalidOperationException(
                "A Contract legal snapshot can only be created after registration finalization or activation.");
    }

    private void SetCurrencyId(long? value)
        => CurrencyId = value.HasValue
            ? Guard.Against.NegativeOrZero(value.Value, nameof(value))
            : null;

    private void SetAmounts(decimal initialAmount, decimal finalAmount)
    {
        InitialAmount = ContractFinancialMath.NormalizeMoney(initialAmount);
        FinalAmount = ContractFinancialMath.NormalizeMoney(finalAmount);

        if (InitialAmount < 0m || FinalAmount < 0m)
            throw new InvalidOperationException(
                "Contract legal snapshot amounts cannot be negative.");

        if (FinalAmount != InitialAmount)
            throw new InvalidOperationException(
                "The Contract legal snapshot final amount must equal its initial amount.");
    }
}
