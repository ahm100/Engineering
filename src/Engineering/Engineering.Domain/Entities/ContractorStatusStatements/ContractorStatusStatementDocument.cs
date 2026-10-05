
namespace Engineering.Domain.Entities.ContractorStatusStatements;

[Description(CSSCmts.ContractorStatusStatementDocument)]
public class ContractorStatusStatementDocument : AuditableEntity<ContractorStatusStatementDocument, long>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(GlobalCmts.ContractorStatusStatement)]
    public long ContractorStatusStatementId { get; private set; }
    public ContractorStatusStatement ContractorStatusStatement { get; private set; }

    [Description(GlobalCmts.ContractorStatusStatementPayment)]
    public long? ContractorStatusStatementPaymentId { get; private set; }
    public ContractorStatusStatementPayment? ContractorStatusStatementPayment { get; private set; }

    public ContractorStatusStatementDocument(
        ContractorStatusStatement cSS,
        ContractorStatusStatementPayment? cSSPayment,
        string url) : this()
    {
        SetContractorStatusStatement(cSS);
        if (cSSPayment is not null)
            SetContractorStatusStatementPayment(cSSPayment);
        SetUrl(url);
    }

    public void SetContractorStatusStatement(ContractorStatusStatement value)
    {
        ContractorStatusStatement = Guard.Against.Null(value, nameof(value));
        ContractorStatusStatementId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetContractorStatusStatementPayment(ContractorStatusStatementPayment value)
    {
        ContractorStatusStatementPayment = Guard.Against.Null(value, nameof(value));
        ContractorStatusStatementPaymentId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.NullOrEmpty(value, nameof(value));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContractorStatusStatementDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
