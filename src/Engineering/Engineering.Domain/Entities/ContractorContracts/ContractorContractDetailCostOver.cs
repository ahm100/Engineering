using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.CostOvers;

namespace Engineering.Domain.Entities.ContractorContracts;

[Description(CCCmts.ContractorContractDetailCostOver)]
public class ContractorContractDetailCostOver : AuditableEntity<ContractorContractDetailCostOver, long>
{
    #region Properties

    [Description(CCCmts.CostOverContractorId)]
    public long ContractorId { get; private set; }
    [Description(CCCmts.Percentage)]
    public decimal Percentage { get; private set; }
    [Description(CCCmts.Amount)]
    public decimal Amount { get; private set; }
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }
    [Description(CCCmts.ContractorStatusStatementCostOver)]
    private List<ContractorStatusStatementCostOver> _contractorStatusStatementCostOvers;
    public IReadOnlyList<ContractorStatusStatementCostOver> ContractorStatusStatementCostOvers => _contractorStatusStatementCostOvers;
    [Description(GlobalCmts.ContractorContractId)]
    public long? ContractorContractId { get; private set; }
    [Description(GlobalCmts.ContractorContract)]
    public ContractorContract? ContractorContract { get; private set; }
    [Description(CCCmts.ContractorContractDetailId)]
    public long? ContractorContractDetailId { get; private set; }
    [Description(CCCmts.ContractorContractDetail)]
    public ContractorContractDetail? ContractorContractDetail { get; private set; }
    [Description(GlobalCmts.CostOverId)]
    public long CostOverId { get; private set; }
    [Description(GlobalCmts.CostOver)]
    public CostOver CostOver { get; private set; }

    #endregion

    public ContractorContractDetailCostOver(
        ContractorContract? contractorContract,
        ContractorContractDetail? contractorContractDetail,
        CostOver costOver,
        long contractorId,
        decimal percentage,
        string? description) : this()
    {
        SetContractorContract(contractorContract);
        SetContractorContractDetail(contractorContractDetail);
        SetCostOver(costOver);
        SetContractorId(contractorId);
        SetPercentageContract(percentage);
        SetDescription(description);

        if (contractorContract is not null)
            SetAmountContract();
        if (contractorContractDetail is not null)
            SetAmountContractDetail();

    }

    #region Commands

    public static ContractorContractDetailCostOver Create(
        ContractorContract? contractorContract,
        ContractorContractDetail? contractorContractDetail,
        CostOver costOver,
        long contractorId,
        decimal percentage,
        string? description
        )
    {
        return new ContractorContractDetailCostOver(
            contractorContract,
            contractorContractDetail,
            costOver,
            contractorId,
            percentage,
            description
            );
    }

    public void SetAmountContractDetail()
    {
        Amount = (ContractorContractDetail!.TotalAmount!.Value / 100m) * Percentage;
    }
    public void SetContractorContract(ContractorContract? value)
    {
        ContractorContract = value;
        ContractorContractId = value?.Id;
    }
    public void SetContractorContractDetail(ContractorContractDetail? value)
    {
        ContractorContractDetail = value;
        ContractorContractDetailId = value?.Id;
    }
    public void SetAmountContract()
    {
        Amount = ContractorContract is null ? ((ContractorContractDetail!.ContractorContract.TotalAmount!.Value / 100m) * Percentage) : (ContractorContract!.TotalAmount!.Value / 100m) * Percentage;
    }
    public void SetContractorId(long value)
    {
        ContractorId = value;
    }
    public void SetCostOver(CostOver value)
    {
        CostOver = Guard.Against.Null(value, nameof(value));
        CostOverId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetPercentageContractDetail(decimal value)
    {
        Percentage = value;
        SetAmountContractDetail();
    }
    public void SetPercentageContract(decimal value)
    {
        Percentage = value;
        SetAmountContract();
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ContractorContractDetailCostOver()
    {
        _contractorStatusStatementCostOvers = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

}
