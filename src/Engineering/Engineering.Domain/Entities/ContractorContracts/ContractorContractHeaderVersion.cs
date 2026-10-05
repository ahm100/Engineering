using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Domain.Entities.ContractorContracts;

[Description(CContractCmts.ContractorContractHeaderVersion)]
public class ContractorContractHeaderVersion : AuditableEntity<ContractorContractHeaderVersion, long>
{

    [Description(CContractCmts.Content)]
    public string Content { get; private set; } = string.Empty;
    [Description(CContractCmts.Version)]
    public int Version { get; private set; }

    [Description(CContractCmts.CreatedPaymentDate)]
    public DateTime? CreatedPaymentDate { get; private set; }

    [Description(CContractCmts.ContractorContractHeaderId)]
    public long ContractorContractHeaderId { get; private set; }
    public ContractorContractHeader ContractorContractHeader { get; private set; }

    [Description(CContractCmts.ContractorStatusStatementId)]
    public long? ContractorStatusStatementId { get; private set; }
    public ContractorStatusStatement? ContractorStatusStatement { get; private set; }



#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ContractorContractHeaderVersion(
        ContractorContractHeader header,
        ContractorStatusStatement statement,
        string content,
        int version,
        DateTime createdPaymentDate) : this()
    {
        SetContractorContractHeader(header);
        SetContractorStatusStatement(statement);

        SetVersion(version + 1);

        SetContent(content);
        SetCreatedPaymentDate(createdPaymentDate);
    }

    public ContractorContractHeaderVersion()
    {

    }


#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #region Commands
    public void SetContractorContractHeader(ContractorContractHeader value)
    {
        ContractorContractHeader = Guard.Against.Null(value, nameof(value));
        ContractorContractHeaderId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetContractorStatusStatement(ContractorStatusStatement value)
    {
        ContractorStatusStatement = value;
        ContractorStatusStatementId = value.Id;
    }
    public void SetVersion(int value)
    {
        Version = Guard.Against.Null(value, nameof(value));
    }
    public void SetContent(string value)
    {
        Content = Guard.Against.Null(value, nameof(value));
    }
    public void SetCreatedPaymentDate(DateTime value)
    {
        CreatedPaymentDate = Guard.Against.Null(value, nameof(value));
    }

    #endregion
}
