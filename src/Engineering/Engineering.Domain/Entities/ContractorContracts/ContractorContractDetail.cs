using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Domain.Entities.ContractorContracts;

[Description(CCCmts.ContractorContractDetail)]
public class ContractorContractDetail : ActivateEntity<ContractorContractDetail, long>
{
    [Description(CCCmts.WorkLoad)]
    public decimal WorkLoad { get; private set; }

    [Description(CCCmts.UnitAmount)]
    public decimal? UnitAmount { get; private set; }

    [Description(CCCmts.ContractCoefficient)]
    public decimal ContractCoefficient { get; private set; } = 1;

    [Description(CCCmts.TotalAmount)]
    public decimal? TotalAmount { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public DateTime? StartDate { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime? EndDate { get; private set; }

    [Description(CCCmts.TotalCostOveredAmount)]
    public decimal? TotalCostOveredAmount { get; private set; }

    [Description(GlobalCmts.ContractorContractId)]
    public long ContractorContractId { get; private set; }

    [Description(GlobalCmts.ContractorContract)]
    public ContractorContract ContractorContract { get; private set; }

    [Description(GlobalCmts.ProjectOperationId)]
    public long? ProjectOperationId { get; private set; }

    [Description(GlobalCmts.ProjectOperation)]
    public ProjectOperation? ProjectOperation { get; private set; }

    public ContractorContractDetail(
        ContractorContract cContract,
        ProjectOperation? pOperation,
        DateTime? startDate,
        DateTime? endDate,
        decimal? workLoad,
        decimal? unitAmount,
        decimal contractCoefficient
        ) : this()
    {
        SetContractorContract(cContract);

        if (pOperation is not null)
            SetProjectOperation(pOperation);

        SetStartDate(startDate ?? cContract.StartDate);
        SetEndDate(endDate ?? cContract.EndDate);

        SetWorkLoad(workLoad ?? pOperation!.Workload);
        SetUnitAmount(unitAmount ?? pOperation?.OperationInfo.BasePrice);
        SetContractCoefficient(contractCoefficient);
        SetTotalAmount();
    }

    public void SetTotalAmountClc()
    {
        if (this.ContractorContractDetailServices.Any(x => x.ProjectOperationDetailContractorService.ProjectServiceDetail is not null))
            WorkLoad = this.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.ProjectServiceDetail!.ProjectService.Volume;
        else
            WorkLoad = this.ContractorContractDetailServices.Where(x => !x.IsDeleted).Sum(x => x.ProjectOperationDetailContractorService.Volume);

        UnitAmount = this.ContractorContractDetailPrices.FirstOrDefault(x => !x.IsDeleted && x.IsActive)!.Price;
        TotalAmount = WorkLoad * UnitAmount;
    }

    public void SetTotalCostOveredAmountClc()
    {
        TotalCostOveredAmount = TotalAmount + _contractorContractDetailCostOvers.Where(x => !x.IsDeleted).Sum(x => x.Amount);
    }

    public void SetProjectOperation(ProjectOperation value)
    {
        ProjectOperation = value;
        ProjectOperationId = value.Id;

        UnitAmount = value.OperationInfo.BasePrice;
        WorkLoad = value.Workload;
    }

    public void SetContractorContract(ContractorContract value)
    {
        ContractorContract = Guard.Against.Null(value, nameof(value));
        ContractorContractId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetUnitAmount(decimal? value)
    {
        UnitAmount = value;
    }
    public void SetContractCoefficient(decimal value)
    {
        ContractCoefficient = Guard.Against.Null(value, nameof(value));
    }
    public void SetWorkLoad(decimal value)
    {
        WorkLoad = Guard.Against.Null(value, nameof(value));
    }
    public void SetStartDate(DateTime? value)
    {
        StartDate = value;
    }
    public void SetEndDate(DateTime? value)
    {
        EndDate = value;
    }

    public void SetTotalAmount()
    {
        if (ContractorContract.ContractorContractType == Enums.ContractorContractType.OperationBased)
        {
            if (UnitAmount is null || UnitAmount <= 0 || WorkLoad <= 0)
            {
                TotalAmount = null;
                return;
            }

            TotalAmount = (UnitAmount.Value * ContractCoefficient) * WorkLoad;
        }

        else if (ContractorContract.ContractorContractType == Enums.ContractorContractType.Service)
        {
            if (UnitAmount is null || UnitAmount <= 0 || WorkLoad <= 0)
            {
                TotalAmount = null;
                return;
            }

            TotalAmount = UnitAmount.Value * WorkLoad;
        }

        else
            TotalAmount = null;
    }

    [Description(CCCmts.ContractorContractDetailPrice)]
    private List<ContractorContractDetailPrice> _contractorContractDetailPrices;
    public IReadOnlyList<ContractorContractDetailPrice> ContractorContractDetailPrices => _contractorContractDetailPrices;
    [Description(CCCmts.ContractorContractDetailService)]
    private List<ContractorContractDetailService> _contractorContractDetailServices;
    public IReadOnlyList<ContractorContractDetailService> ContractorContractDetailServices => _contractorContractDetailServices;
    [Description(CCCmts.ContractorStatusStatementService)]
    private List<ContractorStatusStatementService> _contractorStatusStatementServices;
    public IReadOnlyList<ContractorStatusStatementService> ContractorStatusStatementServices => _contractorStatusStatementServices;
    [Description(CCCmts.ContractorContractDetailCostOver)]
    private List<ContractorContractDetailCostOver> _contractorContractDetailCostOvers;
    public IReadOnlyList<ContractorContractDetailCostOver> ContractorContractDetailCostOvers => _contractorContractDetailCostOvers;

    private ContractorContractDetail()
    {
        _contractorContractDetailPrices = [];
        _contractorContractDetailServices = [];
        _contractorStatusStatementServices = [];
        _contractorContractDetailCostOvers = [];
    }

}
