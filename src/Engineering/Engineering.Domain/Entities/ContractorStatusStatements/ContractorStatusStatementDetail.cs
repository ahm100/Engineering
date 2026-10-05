using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

[Description(CSSCmts.ContractorStatusStatementDetail)]
public class ContractorStatusStatementDetail : AuditableEntity<ContractorStatusStatementDetail>
{

    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }

    [Description(CSSCmts.TotalAmount)]
    public decimal? TotalAmount { get; private set; }

    [Description(CSSCmts.FixedContractPct)]
    public decimal? FixedContractPct { get; private set; }

    [Description(CSSCmts.FixedContractPctAmount)]
    public decimal? FixedContractPctAmount { get; private set; }

    [Description(CSSCmts.FixedContractPctDesc)]
    public string? FixedContractPctDesc { get; private set; }

    [Description(CSSCmts.ProjectFixedContractPct)]
    public decimal? ProjectFixedContractPct { get; private set; }

    [Description(CSSCmts.ProjectFixedContractPctAmount)]
    public decimal? ProjectFixedContractPctAmount { get; private set; }

    [Description(CSSCmts.ProjectFixedContractPctDesc)]
    public string? ProjectFixedContractPctDesc { get; private set; }

    [Description(CSSCmts.ManagerFixedContractPct)]
    public decimal? ManagerFixedContractPct { get; private set; }

    [Description(CSSCmts.ManagerFixedContractPctAmount)]
    public decimal? ManagerFixedContractPctAmount { get; private set; }

    [Description(CSSCmts.ManagerFixedContractPctDesc)]
    public string? ManagerFixedContractPctDesc { get; private set; }

    [Description(CSSCmts.PercentageDoingJobWell)]
    public decimal? PercentageDoingJobWell { get; private set; }

    [Description(CSSCmts.DoingJobWellAmount)]
    public decimal? DoingJobWellAmount { get; private set; }

    [Description(CSSCmts.PercentageAdvancePayment)]
    public decimal? PercentageAdvancePayment { get; private set; }

    [Description(CSSCmts.AdvancePaymentAmount)]
    public decimal? AdvancePaymentAmount { get; private set; }

    [Description(CSSCmts.DailyLatenessPenalty)]
    public decimal? DailyLatenessPenalty { get; private set; }

    [Description(CSSCmts.WorkDonePercent)]
    public int? WorkDonePercent { get; private set; }

    [Description(CSSCmts.WorkDeliveryPercent)]
    public int? WorkDeliveryPercent { get; private set; }

    [Description(CSSCmts.WorkCompletionPercent)]
    public int? WorkCompletionPercent { get; private set; }

    [Description(CSSCmts.Description)]
    public string? Description { get; private set; }

    [Description(CSSCmts.ContractorContract)]
    public ContractorContract ContractorContract { get; private set; }
    public long ContractorContractId { get; private set; }

    [Description(CSSCmts.ContractorStatusStatement)]
    public ContractorStatusStatement ContractorStatusStatement { get; private set; }
    public long ContractorStatusStatementId { get; private set; }

    public ContractorStatusStatementDetail(
        ContractorStatusStatement contractorStatusStatement,
        ContractorContract contractorContract,
        DateTime startDate,
        DateTime endDate,
        decimal? totalAmount,
        decimal? fixedContractPct,
        string? fixedContractPctDesc,
        decimal? percentageDoingJobWell,
        decimal? doingJobWellAmount,
        decimal? percentageAdvancePayment,
        decimal? advancePaymentAmount,
        decimal? dailyLatenessPenalty,
        int? workDonePercent,
        int? workDeliveryPercent,
        int? workCompletionPercent,
        string? description
        ) : this()
    {
        ContractorStatusStatement = Guard.Against.Null(contractorStatusStatement, nameof(contractorStatusStatement));
        ContractorContract = Guard.Against.Null(contractorContract, nameof(contractorContract));
        StartDate = Guard.Against.Null(startDate, nameof(startDate));
        EndDate = Guard.Against.Null(endDate, nameof(endDate));
        TotalAmount = totalAmount;
        PercentageDoingJobWell = percentageDoingJobWell;
        DoingJobWellAmount = doingJobWellAmount;
        PercentageAdvancePayment = percentageAdvancePayment;
        AdvancePaymentAmount = advancePaymentAmount;
        DailyLatenessPenalty = dailyLatenessPenalty;
        WorkDonePercent = workDonePercent;
        WorkDeliveryPercent = workDeliveryPercent;
        WorkCompletionPercent = workCompletionPercent;
        Description = description;

        if (contractorContract.ContractorContractType == ContractorContractType.Fixed)
            SetFixedContractPct(fixedContractPct ?? 100, fixedContractPctDesc);
    }

    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
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

    public void SetProjectFixedContractPctDesc(string? value)
    {
        ProjectFixedContractPctDesc = value;
    }

    public void SetManagerFixedContractPctDesc(string? value)
    {
        ManagerFixedContractPctDesc = value;
    }

    public void SetFixedContractPctDescription(string? value)
    {
        FixedContractPctDesc = value;
    }

    public void SetDelete()
    {
        IsDeleted = true;
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

    public void SetTotalAmount(decimal? value)
    {
        TotalAmount = value;
    }

    public void SetFixedContractPct(decimal? ptc, string? desc)
    {
        FixedContractPct = ptc;
        FixedContractPctDesc = desc;
        var totalAmount = (TotalAmount / 100) * ptc;
        FixedContractPctAmount = totalAmount;
    }

    public void SetProjectContractPct(decimal? ptc, string? desc)
    {
        ProjectFixedContractPct = ptc;
        ProjectFixedContractPctDesc = desc;
        var totalAmount = (FixedContractPctAmount / 100) * ptc;
        ProjectFixedContractPctAmount = totalAmount;
    }

    public void SetManagerContractPct(decimal? ptc, string? desc)
    {
        ManagerFixedContractPct = ptc;
        ManagerFixedContractPctDesc = desc;
        var totalAmount = (ProjectFixedContractPctAmount / 100) * ptc;
        ManagerFixedContractPctAmount = totalAmount;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<ContractorStatusStatementService> _contractorStatusStatementServices;
    public IReadOnlyList<ContractorStatusStatementService> ContractorStatusStatementServices => _contractorStatusStatementServices;
    private ContractorStatusStatementDetail()
    {
        _contractorStatusStatementServices = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
