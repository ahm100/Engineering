namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractChangeDocument)]
public class ContractChangeDocument
    : AuditableEntity<ContractChangeDocument, long>
{
    public long ContractChangeId { get; private set; }
    public ContractChange ContractChange { get; private set; } = null!;

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    private ContractChangeDocument()
    {
    }

    public ContractChangeDocument(ContractChange contractChange, string url)
    {
        SetContractChange(contractChange);
        SetUrl(url);
    }

    private void SetContractChange(ContractChange value)
    {
        ContractChange = Guard.Against.Null(value, nameof(value));
        ContractChangeId = value.Id;
    }

    private void SetUrl(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value));

        if (value.Length > 1500)
            throw new ArgumentException("Url cannot exceed 1500 characters.", nameof(value));

        Url = value;
    }
}
