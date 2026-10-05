namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractAdjustmentIndex)]
public class ContractAdjustmentIndex
    : AuditableEntity<ContractAdjustmentIndex, long>
{
    [Description(ContractCmts.ContractAdjustmentReferenceId)]
    public long ContractAdjustmentReferenceId { get; private set; }

    public ContractAdjustmentReference ContractAdjustmentReference
    {
        get;
        private set;
    } = null!;

    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(GlobalCmts.FaTitle)]
    public string FaTitle { get; private set; } = string.Empty;

    [Description(GlobalCmts.EnTitle)]
    public string EnTitle { get; private set; } = string.Empty;

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    private ContractAdjustmentIndex()
    {
    }

    public ContractAdjustmentIndex(
        ContractAdjustmentReference contractAdjustmentReference,
        string code,
        string faTitle,
        string enTitle,
        string? description)
    {
        SetContractAdjustmentReference(contractAdjustmentReference);
        SetCode(code);
        SetFaTitle(faTitle);
        SetEnTitle(enTitle);
        SetDescription(description);
        IsActive = true;
    }

    public void Update(
        string code,
        string faTitle,
        string enTitle,
        string? description)
    {
        SetCode(code);
        SetFaTitle(faTitle);
        SetEnTitle(enTitle);
        SetDescription(description);
    }

    public void Activate()
        => IsActive = true;

    public void Deactivate()
        => IsActive = false;

    private void SetContractAdjustmentReference(
        ContractAdjustmentReference value)
    {
        ContractAdjustmentReference =
            Guard.Against.Null(value, nameof(value));

        ContractAdjustmentReferenceId = value.Id;
    }

    private void SetCode(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value));

        if (value.Length > 100)
            throw new ArgumentException(
                "Code cannot exceed 100 characters.",
                nameof(value));

        Code = value;
    }

    private void SetFaTitle(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value));

        if (value.Length > 250)
            throw new ArgumentException(
                "FaTitle cannot exceed 250 characters.",
                nameof(value));

        FaTitle = value;
    }

    private void SetEnTitle(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value));

        if (value.Length > 250)
            throw new ArgumentException(
                "EnTitle cannot exceed 250 characters.",
                nameof(value));

        EnTitle = value;
    }

    private void SetDescription(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Description = null;
            return;
        }

        if (value.Length > 1500)
            throw new ArgumentException(
                "Description cannot exceed 1500 characters.",
                nameof(value));

        Description = value;
    }
}
