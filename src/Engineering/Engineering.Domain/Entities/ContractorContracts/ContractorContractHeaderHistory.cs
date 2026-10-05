using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Domain.Entities.ContractorContracts;

[Description(CCCmts.ContractorContractHeaderHistory)]
public class ContractorContractHeaderHistory : AuditableEntity<ContractorContractHeaderHistory, long>
{
    #region Properties
    [Description(GlobalCmts.ContractorId)]
    public long ContractorId { get; private set; }
    [Description(CCCmts.ContractorContractStatus)]
    public ContractorContractStatus Status { get; private set; } = ContractorContractStatus.New;
    [Description(CCCmts.CurrencyId)]
    public long CurrencyId { get; private set; }
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }
    [Description(CCCmts.MinStartDate)]
    public DateTime? StartDate { get; private set; }
    [Description(CCCmts.MaxEndDate)]
    public DateTime? EndDate { get; private set; }
    [Description(CCCmts.FinalTotalAmount)]
    public decimal? FinalTotalAmount { get; private set; }
    [Description(CCCmts.TotalPercentageDoingJobWell)]
    public decimal? TotalPercentageDoingJobWell { get; private set; }
    [Description(CCCmts.TotalDoingJobWellAmount)]
    public decimal? TotalDoingJobWellAmount { get; private set; }
    [Description(CCCmts.TotalPercentageAdvancePayment)]
    public decimal? TotalPercentageAdvancePayment { get; private set; }
    [Description(CCCmts.TotalAdvancePaymentAmount)]
    public decimal? TotalAdvancePaymentAmount { get; private set; }
    [Description(CCCmts.TotalDailyLatenessPenalty)]
    public decimal? TotalDailyLatenessPenalty { get; private set; }
    [Description(CCCmts.TotalWorkDonePercent)]
    public decimal? TotalWorkDonePercent { get; private set; }
    [Description(CCCmts.TotalWorkDeliveryPercent)]
    public decimal? TotalWorkDeliveryPercent { get; private set; }
    [Description(CCCmts.TotalWorkCompletionPercent)]
    public decimal? TotalWorkCompletionPercent { get; private set; }

    [Description(CCCmts.ContractorContractHeaderId)]
    public long ContractorContractHeaderId { get; private set; } = default!;
    public ContractorContractHeader ContractorContractHeader { get; private set; } = default!;

    #endregion

    #region Constructors

    public ContractorContractHeaderHistory(
        ContractorContractHeader contractorContractHeader,
        long contractorId,
        ContractorContractStatus status,
        long currencyId,
        string? description,
        DateTime? startDate,
        DateTime? endDate,
        decimal? finalTotalAmount,
        decimal? totalPercentageDoingJobWell,
        decimal? totalDoingJobWellAmount,
        decimal? totalPercentageAdvancePayment,
        decimal? totalAdvancePaymentAmount,
        decimal? totalDailyLatenessPenalty,
        decimal? totalWorkDonePercent,
        decimal? totalWorkDeliveryPercent,
        decimal? totalWorkCompletionPercent
        ) : this()
    {
        SetContractorContractHeader(contractorContractHeader);
        SetContractorId(contractorId);
        SetStatus(status);
        SetCurrencyId(currencyId);
        SetDescription(description);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetFinalTotalAmount(finalTotalAmount);
        SetTotalPercentageDoingJobWell(totalPercentageDoingJobWell);
        SetTotalDoingJobWellAmount(totalDoingJobWellAmount);
        SetTotalPercentageAdvancePayment(totalPercentageAdvancePayment);
        SetTotalAdvancePaymentAmount(totalAdvancePaymentAmount);
        SetTotalDailyLatenessPenalty(totalDailyLatenessPenalty);
        SetTotalWorkDonePercent(totalWorkDonePercent);
        SetTotalWorkDeliveryPercent(totalWorkDeliveryPercent);
        SetTotalWorkCompletionPercent(totalWorkCompletionPercent);
    }

    public static ContractorContractHeaderHistory Create(
        ContractorContractHeader contractorContractHeader,
        long contractorId,
        ContractorContractStatus status,
        long currencyId,
        string? description,
        DateTime? startDate,
        DateTime? endDate,
        decimal? finalTotalAmount,
        decimal? totalPercentageDoingJobWell,
        decimal? totalDoingJobWellAmount,
        decimal? totalPercentageAdvancePayment,
        decimal? totalAdvancePaymentAmount,
        decimal? totalDailyLatenessPenalty,
        decimal? totalWorkDonePercent,
        decimal? totalWorkDeliveryPercent,
        decimal? totalWorkCompletionPercent)
    {
        return new ContractorContractHeaderHistory(
            contractorContractHeader,
            contractorId,
            status,
            currencyId,
            description,
            startDate,
            endDate,
            finalTotalAmount,
            totalPercentageDoingJobWell,
            totalDoingJobWellAmount,
            totalPercentageAdvancePayment,
            totalAdvancePaymentAmount,
            totalDailyLatenessPenalty,
            totalWorkDonePercent,
            totalWorkDeliveryPercent,
            totalWorkCompletionPercent);
    }

    public ContractorContractHeaderHistory()
    {

    }

    #endregion

    #region Commands
    public void SetContractorId(long value)
    {
        ContractorId = Guard.Against.Null(value, nameof(value));
    }
    public void SetContractorContractHeader(ContractorContractHeader value)
    {
        ContractorContractHeader = Guard.Against.Null(value, nameof(value));
        ContractorContractHeaderId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetStatus(ContractorContractStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public void SetCurrencyId(long value)
    {
        CurrencyId = Guard.Against.Null(value, nameof(value));
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetStartDate(DateTime? value)
    {
        StartDate = value;
    }

    public void SetEndDate(DateTime? value)
    {
        EndDate = value;
    }

    public void SetFinalTotalAmount(decimal? value)
    {
        FinalTotalAmount = value;
    }

    public void SetTotalPercentageDoingJobWell(decimal? value)
    {
        TotalPercentageDoingJobWell = value;
    }

    public void SetTotalDoingJobWellAmount(decimal? value)
    {
        TotalDoingJobWellAmount = value;
    }

    public void SetTotalPercentageAdvancePayment(decimal? value)
    {
        TotalPercentageAdvancePayment = value;
    }

    public void SetTotalAdvancePaymentAmount(decimal? value)
    {
        TotalAdvancePaymentAmount = value;
    }

    public void SetTotalDailyLatenessPenalty(decimal? value)
    {
        TotalDailyLatenessPenalty = value;
    }

    public void SetTotalWorkDonePercent(decimal? value)
    {
        TotalWorkDonePercent = value;
    }

    public void SetTotalWorkDeliveryPercent(decimal? value)
    {
        TotalWorkDeliveryPercent = value;
    }

    public void SetTotalWorkCompletionPercent(decimal? value)
    {
        TotalWorkCompletionPercent = value;
    }
    #endregion
}



