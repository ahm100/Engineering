
namespace Engineering.Domain.Entities.ContractorContracts;
/// <summary>
/// مستندات پیوست قرارداد پیمانکار
/// </summary>
[Description(CCCmts.ContractorContractHeaderDocument)]
public class ContractorContractHeaderDocument : AuditableEntity<ContractorContractHeaderDocument, long>
{
    [Description(CCCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(CCCmts.ContractorContractHeaderId)]
    public long ContractorContractHeaderId { get; private set; }
    public ContractorContractHeader ContractorContractHeader { get; private set; }

    public ContractorContractHeaderDocument(
        string url,
        ContractorContractHeader contractorContractHeader) : this()
    {
        SetUrl(url);
        SetContractorContractHeader(contractorContractHeader);
    }

    public static ContractorContractHeaderDocument Create(
        string url,
        ContractorContractHeader requestGoodsSupplyDetail)
    {
        return new ContractorContractHeaderDocument(url, requestGoodsSupplyDetail);
    }

    public void SetContractorContractHeader(ContractorContractHeader value)
    {
        ContractorContractHeader = Guard.Against.Null(value, nameof(value));
        ContractorContractHeaderId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetUrl(string value)
    {
        Url = Guard.Against.Null(value, nameof(value));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContractorContractHeaderDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
