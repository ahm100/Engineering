using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetCContractHeaderById;

public class GetCCHByIdResponse
{
    public long Id { get; set; }
    public long? VersionId { get; set; }
    public int Version { get; set; } = 0;
    public bool HaveAVersion { get; set; } = false;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long ContractorId { get; set; }
    public string? Contractor { get; set; }
    public ContractorContractStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public long CurrencyId { get; set; }
    public string? Currency { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? FinalTotalAmount { get; set; }
    public decimal? TotalPercentageDoingJobWell { get; set; }
    public decimal? TotalDoingJobWellAmount { get; set; }
    public decimal? TotalAdvancePaymentAmount { get; set; }
    public decimal? TotalPercentageAdvancePayment => TotalAdvancePaymentAmount == 0 || FinalTotalAmount == 0 ? 0 : (TotalAdvancePaymentAmount / FinalTotalAmount) * 100;
    public decimal? TotalDailyLatenessPenalty { get; set; }
    public decimal? TotalWorkDonePercent { get; set; }
    public decimal? TotalWorkDeliveryPercent { get; set; }
    public decimal? TotalWorkCompletionPercent { get; set; }
    public string? Description { get; set; }
    public bool HasFixed { get; set; }
    public bool HasService { get; set; }
    public bool HasOperation { get; set; }
    public bool HasProfessionalWorkday { get; set; }
    public List<string>? Urls { get; set; }
}