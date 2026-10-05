using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

/// <summary>
/// شرح عملیات پروژه صورت وضعیت پیمانکار
/// </summary>
public class ContractorStatusStatementCostOver : AuditableEntity<ContractorStatusStatementCostOver>
{
    [Description(CSSCmts.Amount)]
    public decimal Amount { get; private set; }

    [Description(CSSCmts.Description)]
    public string? Description { get; private set; }

    [Description(CSSCmts.ContractorContractDetailCostOver)]
    public ContractorContractDetailCostOver ContractorContractDetailCostOver { get; private set; }
    public long ContractorContractDetailCostOverId { get; private set; }

    [Description(CSSCmts.ContractorStatusStatement)]
    public ContractorStatusStatement ContractorStatusStatement { get; private set; }
    public long ContractorStatusStatementId { get; private set; }

    public ContractorStatusStatementCostOver(
        ContractorStatusStatement contractorStatusStatement,
        ContractorContractDetailCostOver contractorContractDetailCostOver,
        decimal amount,
        string? description
        ) : this()
    {
        ContractorStatusStatement = Guard.Against.Null(contractorStatusStatement, nameof(contractorStatusStatement));
        ContractorContractDetailCostOver = Guard.Against.Null(contractorContractDetailCostOver, nameof(contractorContractDetailCostOver));
        Amount = Guard.Against.Null(amount, nameof(amount));
        Description = description;
    }

    public static ContractorStatusStatementCostOver Create(
        ContractorStatusStatement contractorStatusStatement,
        ContractorContractDetailCostOver contractorContractDetailCostOver,
        decimal amount,
        string? description)
    {
        return new ContractorStatusStatementCostOver(
            contractorStatusStatement,
            contractorContractDetailCostOver,
            amount,
            description);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContractorStatusStatementCostOver()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
