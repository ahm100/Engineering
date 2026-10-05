using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.ContractorContracts;

[Description(GlobalCmts.ContractorContract)]
public class ContractorContract : AuditableEntity<ContractorContract, long>
{
    [Description(GlobalCmts.CompanyId)]
    public long CompanyId { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }

    [Description(CCCmts.TotalAmount)]
    public decimal? TotalAmount { get; private set; }

    [Description(CCCmts.PercentageDoingJobWell)]
    public decimal? PercentageDoingJobWell { get; private set; }

    [Description(CCCmts.DoingJobWellAmount)]
    public decimal? DoingJobWellAmount { get; private set; }

    [Description(CCCmts.PercentageAdvancePayment)]
    public decimal? PercentageAdvancePayment { get; private set; }

    [Description(CCCmts.AdvancePaymentAmount)]
    public decimal? AdvancePaymentAmount { get; private set; }

    [Description(CCCmts.DailyLatenessPenalty)]
    public decimal? DailyLatenessPenalty { get; private set; }

    [Description(CCCmts.WorkDonePercent)]
    public int? WorkDonePercent { get; private set; }

    [Description(CCCmts.WorkDeliveryPercent)]
    public int? WorkDeliveryPercent { get; private set; }

    [Description(CCCmts.WorkCompletionPercent)]
    public int? WorkCompletionPercent { get; private set; }

    [Description(CCCmts.TotalCostOveredAmount)]
    public decimal? TotalCostOveredAmount { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.Project)]
    public long? ProjectId { get; private set; }
    public Project? Project { get; private set; }

    [Description(CCCmts.ContractorContractHeader)]
    public long ContractorContractHeaderId { get; private set; }
    public ContractorContractHeader ContractorContractHeader { get; set; }

    [Description(CCCmts.ContractorContractType)]
    public ContractorContractType ContractorContractType { get; set; }

    [Description(CCCmts.DailyBaseHours)]
    public decimal? DailyBaseHours { get; private set; }

    [Description(CCCmts.MonthlyBaseHours)]
    public decimal? MonthlyBaseHours { get; private set; }

    #region Constructors
    public ContractorContract(
        long companyId,
        ContractorContractHeader contractorContractHeader,
        ContractorContractType contractorContractType,
        Project project,
        DateTime startDate,
        DateTime endDate,
        decimal? totalAmount,
        decimal? percentageDoingJobWell,
        decimal? doingJobWellAmount,
        decimal? percentageAdvancePayment,
        decimal? advancePaymentAmount,
        decimal? dailyLatenessPenalty,
        int? workDonePercent,
        int? workDeliveryPercent,
        int? workCompletionPercent,
        decimal? dailyBaseHours,
        decimal? monthlyBaseHours,
        string? description
        ) : this()
    {
        SetCompany(companyId);
        SetContractorContractHeader(contractorContractHeader);
        SetContractorContractType(contractorContractType);
        SetProject(project);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetTotalAmount(totalAmount);
        SetPercentageDoingJobWell(percentageDoingJobWell);
        SetDoingJobWellAmount(doingJobWellAmount);
        SetAdvancePaymentAmount(percentageAdvancePayment);
        SetAdvancePaymentAmount(advancePaymentAmount);
        SetDailyLatenessPenalty(dailyLatenessPenalty);
        SetWorkDonePercent(workDonePercent);
        SetWorkDeliveryPercent(workDeliveryPercent);
        SetWorkCompletionPercent(workCompletionPercent);
        SetDescription(description);
        SetDailyBaseHours(dailyBaseHours);
        SetMonthlyBaseHours(monthlyBaseHours);

        AddHistory();
    }

    #endregion

    #region Commands

    private void SetCompany(long value)
        => CompanyId = Guard.Against.NegativeOrZero(value, nameof(value));

    public void AddHistory()
    {
        _histories.Add(ContractorContractHistory.Create(WorkDonePercent, WorkDeliveryPercent, WorkCompletionPercent, Description, this));
    }
    public void SetContractorContractHeader(ContractorContractHeader value)
    {
        ContractorContractHeader = Guard.Against.Null(value, nameof(value));
        ContractorContractHeaderId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetContractorContractType(ContractorContractType value)
    {
        ContractorContractType = Guard.Against.Null(value, nameof(value));
    }
    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetTotalAmount(decimal? value)
    {
        if (ContractorContractType ==
            ContractorContractType.OperationBased)
        {
            var activeDetails = Details
                .Where(x => !x.IsDeleted)
                .ToList();

            if (activeDetails.Count == 0)
            {
                TotalAmount = value;
                return;
            }

            TotalAmount =
                activeDetails.Sum(x =>
                    x.TotalAmount ?? 0);

            return;
        }

        TotalAmount = value;
    }

    public void SetTotalAmountClc()
    {
        TotalAmount = this.Details.Where(x => !x.IsDeleted).Sum(x => x.TotalAmount);
    }

    public void SetPercentageDoingJobWell(decimal? value)
    {
        PercentageDoingJobWell = value;
    }

    public void SetDoingJobWellAmount(decimal? value)
    {
        DoingJobWellAmount = value;
    }

    public void SetPercentageAdvancePayment(decimal? value)
    {
        PercentageAdvancePayment = value;
    }

    public void SetAdvancePaymentAmount(decimal? value)
    {
        AdvancePaymentAmount = value;
    }

    public void SetDailyLatenessPenalty(decimal? value)
    {
        DailyLatenessPenalty = value;
    }

    public void SetContractorContractDescription(string? value)
    {
        Description = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetWorkDonePercent(int? value)
    {
        WorkDonePercent = value;
    }

    public void SetWorkDeliveryPercent(int? value)
    {
        WorkDeliveryPercent = value;
    }

    public void SetWorkCompletionPercent(int? value)
    {
        WorkCompletionPercent = value;
    }

    public void SetDailyBaseHours(decimal? value)
    {
        DailyBaseHours = value;
    }

    public void SetMonthlyBaseHours(decimal? value)
    {
        MonthlyBaseHours = value;
    }

    public void SetTotalCostOveredAmountClc()
    {
        TotalCostOveredAmount = TotalAmount + _contractorContractDetailCostOvers.Sum(x => x.Amount);
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(CCCmts.ContractorContractHistory)]
    private List<ContractorContractHistory> _histories;
    public IReadOnlyList<ContractorContractHistory> Histories => _histories;

    [Description(CCCmts.ContractorContractDetail)]
    private List<ContractorContractDetail> _details;
    public IReadOnlyList<ContractorContractDetail> Details => _details;

    [Description(CCCmts.ContractorStatusStatementDetail)]
    private List<ContractorStatusStatementDetail> _contractorStatusStatementDetails;
    public IReadOnlyList<ContractorStatusStatementDetail> ContractorStatusStatementDetails => _contractorStatusStatementDetails;

    [Description(CCCmts.ContractorContractDetailCostOver)]
    private List<ContractorContractDetailCostOver> _contractorContractDetailCostOvers;
    public IReadOnlyList<ContractorContractDetailCostOver> ContractorContractDetailCostOvers => _contractorContractDetailCostOvers;

    private ContractorContract()
    {
        _histories = [];
        _details = [];
        _contractorStatusStatementDetails = [];
        _contractorContractDetailCostOvers = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
