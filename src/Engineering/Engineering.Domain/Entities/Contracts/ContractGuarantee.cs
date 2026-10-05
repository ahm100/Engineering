using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractGuarantee)]
public class ContractGuarantee : AuditableEntity<ContractGuarantee, long>
{
    #region Properties

    [Description(GlobalCmts.ContractId)]
    public long ContractId { get; private set; }

    public Contract Contract { get; private set; } = null!;

    [Description(ContractCmts.ContractGuaranteeType)]
    public ContractGuaranteeType Type { get; private set; }

    [Description(ContractCmts.ContractGuaranteeAmount)]
    public decimal Amount { get; private set; }

    [Description(ContractCmts.ContractGuaranteePercentage)]
    public decimal? Percentage { get; private set; }

    [Description(ContractCmts.ContractGuaranteeNumber)]
    public string Number { get; private set; } = string.Empty;

    [Description(ContractCmts.ContractGuaranteeIssueDate)]
    public DateTime IssueDate { get; private set; }

    [Description(ContractCmts.ContractGuaranteeExpiryDate)]
    public DateTime ExpiryDate { get; private set; }

    [Description(ContractCmts.ContractGuaranteeStatus)]
    public ContractGuaranteeStatus Status { get; private set; }

    [Description(ContractCmts.ContractGuaranteeFileUrl)]
    public string? FileUrl { get; private set; }

    #endregion

    #region Constructors

    private ContractGuarantee()
    {
    }

    public ContractGuarantee(
        Contract contract,
        ContractGuaranteeType type,
        decimal amount,
        decimal? percentage,
        string number,
        DateTime issueDate,
        DateTime expiryDate,
        string? fileUrl)
    {
        SetContract(contract);
        Status = ContractGuaranteeStatus.Active;
        Update(type, amount, percentage, number, issueDate, expiryDate, fileUrl);
    }

    #endregion

    #region Commands

    public void Update(
        ContractGuaranteeType type,
        decimal amount,
        decimal? percentage,
        string number,
        DateTime issueDate,
        DateTime expiryDate,
        string? fileUrl)
    {
        Type = Guard.Against.EnumOutOfRange(type, nameof(type));
        SetAmount(amount);
        SetPercentage(percentage);
        SetNumber(number);
        SetDates(issueDate, expiryDate);
        SetFileUrl(fileUrl);
    }

    public void ChangeStatus(ContractGuaranteeStatus status)
    {
        Status = Guard.Against.EnumOutOfRange(status, nameof(status));
    }

    #endregion

    #region Private Helpers

    private void SetContract(Contract value)
    {
        Contract = Guard.Against.Null(value, nameof(value));
        ContractId = Guard.Against.NegativeOrZero(value.Id, nameof(value.Id));
    }

    private void SetAmount(decimal value)
    {
        value = ContractFinancialMath.NormalizeMoney(value);
        Amount = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    private void SetPercentage(decimal? value)
    {
        if (!value.HasValue)
        {
            Percentage = null;
            return;
        }

        var normalized = ContractFinancialMath.NormalizePercentage(value.Value);
        Guard.Against.NegativeOrZero(normalized, nameof(value));

        if (normalized > 100m)
            throw new ArgumentOutOfRangeException(nameof(value), "Guarantee percentage cannot be greater than 100.");

        Percentage = normalized;
    }

    private void SetNumber(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value)).Trim();

        if (value.Length > 250)
            throw new ArgumentException("Guarantee number cannot exceed 250 characters.", nameof(value));

        Number = value;
    }

    private void SetDates(DateTime issueDate, DateTime expiryDate)
    {
        if (issueDate == default)
            throw new ArgumentException("IssueDate is required.", nameof(issueDate));

        if (expiryDate == default)
            throw new ArgumentException("ExpiryDate is required.", nameof(expiryDate));

        if (expiryDate.Date < issueDate.Date)
            throw new ArgumentException("ExpiryDate cannot be before IssueDate.", nameof(expiryDate));

        IssueDate = issueDate;
        ExpiryDate = expiryDate;
    }

    private void SetFileUrl(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            FileUrl = null;
            return;
        }

        value = value.Trim();

        if (value.Length > 1500)
            throw new ArgumentException("Guarantee file URL cannot exceed 1500 characters.", nameof(value));

        FileUrl = value;
    }
    #endregion

}
