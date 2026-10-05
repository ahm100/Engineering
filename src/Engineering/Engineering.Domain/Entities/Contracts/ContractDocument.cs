namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractDocument)]
public class ContractDocument : AuditableEntity<ContractDocument, long>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(GlobalCmts.ContractId)]
    public long ContractId { get; private set; }

    public Contract Contract { get; private set; } = null!;

    private ContractDocument()
    {
    }

    public ContractDocument(
        string url,
        Contract contract)
    {
        SetUrl(url);
        SetContract(contract);
    }

    private void SetUrl(string value)
    {
        Url = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    private void SetContract(Contract value)
    {
        Contract = Guard.Against.Null(value, nameof(value));
        ContractId = Contract.Id;
    }
}