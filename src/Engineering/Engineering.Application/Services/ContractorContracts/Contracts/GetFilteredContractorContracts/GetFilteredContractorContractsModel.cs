
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContracts;

public record GetFilteredContractorContractsModel
{
    public long Id { get; set; }
    public long ContractorContractTypeId { get; set; }
    public string? ContractorContractTypeName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? PercentageDoingJobWell { get; set; }
    public decimal? DoingJobWellAmount { get; set; }
    public decimal? PercentageAdvancePayment { get; set; }
    public decimal? AdvancePaymentAmount { get; set; }
    public decimal? DailyLatenessPenalty { get; set; }
    public int? WorkDonePercent { get; set; }
    public int? WorkDeliveryPercent { get; set; }
    public int? WorkCompletionPercent { get; set; }
    public string? Description { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
