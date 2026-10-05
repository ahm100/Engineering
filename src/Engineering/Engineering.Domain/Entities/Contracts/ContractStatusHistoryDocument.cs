namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractStatusHistoryDocument)]
public class ContractStatusHistoryDocument
    : AuditableEntity<ContractStatusHistoryDocument, long>
{
    public long ContractStatusHistoryId { get; private set; }
    public ContractStatusHistory ContractStatusHistory { get; private set; } = null!;

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    private ContractStatusHistoryDocument()
    {
    }

    public ContractStatusHistoryDocument(
        ContractStatusHistory contractStatusHistory,
        string url)
    {
        SetContractStatusHistory(contractStatusHistory);
        SetUrl(url);
    }

    private void SetContractStatusHistory(ContractStatusHistory value)
    {
        ContractStatusHistory = Guard.Against.Null(value, nameof(value));
        ContractStatusHistoryId = value.Id;
    }

    private void SetUrl(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value));

        if (value.Length > 1500)
            throw new ArgumentException("Url cannot exceed 1500 characters.", nameof(value));

        Url = value;
    }
}
