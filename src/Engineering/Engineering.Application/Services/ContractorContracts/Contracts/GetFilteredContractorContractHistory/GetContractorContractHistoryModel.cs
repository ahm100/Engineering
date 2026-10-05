using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractHistory;

public record GetContractorContractHistoryModel
{
    public long Id { get; set; }
    public ContractorContractStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? FinalTotalAmount { get; set; }
    public decimal? TotalPercentageDoingJobWell { get; set; }
    public decimal? TotalDoingJobWellAmount { get; set; }
    public decimal? TotalPercentageAdvancePayment { get; set; }
    public decimal? TotalAdvancePaymentAmount { get; set; }
    public decimal? TotalDailyLatenessPenalty { get; set; }
    public decimal? TotalWorkDonePercent { get; set; }
    public decimal? TotalWorkDeliveryPercent { get; set; }
    public decimal? TotalWorkCompletionPercent { get; set; }
    public string? Description { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}

