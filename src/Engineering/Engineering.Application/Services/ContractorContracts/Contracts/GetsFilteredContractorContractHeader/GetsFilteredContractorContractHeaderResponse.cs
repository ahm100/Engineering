using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader;

public record GetsFilteredContractorContractHeaderResponse(
    List<GetsFilteredContractorContractHeaderModel> Data,
    int RowCount
    );

public record GetsFilteredContractorContractHeaderModel
{
    public long Id { get; set; }
    public int Version { get; set; } = 0;
    public bool HaveAVersion { get; set; } = false;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public long? ProjectId => Contracts.FirstOrDefault()?.ProjectId;
    public string? ProjectName => Contracts.Select(x => x.ProjectName).Distinct().JoinList();
    public string? ProjectCode => Contracts.Select(x => x.ProjectCode).Distinct().JoinList();
    public long? ProjectManagerId => Contracts.FirstOrDefault()?.ProjectManagerId;
    public string? ProjectManager { get; set; }
    public long? CompanyId { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public bool HaveStatusStatement { get; set; }
    public bool ReviewAble { get; set; }
    public ContractorContractStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public long ContractorId { get; set; }
    public string? Contractor { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public string? Description { get; set; }
    public string? ProjectManagerDescription { get; set; }
    public bool HavePrices { get; set; }
    public DateTime? StartDate => Contracts.Count == 0 ? null : Contracts.Min(x => x.StartDate);
    public DateTime? EndDate => Contracts.Count == 0 ? null : Contracts.Max(x => x.StartDate);
    public decimal? FinalTotalAmount => Contracts.Count == 0 ? null : Contracts.Sum(x => x.TotalAmount);
    public decimal? TotalPercentageDoingJobWell => Contracts.Count == 0
        ? null
        : Contracts.Sum(x => x.PercentageDoingJobWell) / Contracts.Count;
    public decimal? TotalDoingJobWellAmount => Contracts.Count == 0
        ? null
        : Contracts.Sum(x => x.DoingJobWellAmount) / Contracts.Count;
    public decimal? TotalAdvancePaymentAmount => Contracts.Count == 0
        ? null
        : Contracts.Sum(x => x.AdvancePaymentAmount);
    public decimal? TotalPercentageAdvancePayment => TotalAdvancePaymentAmount == 0 || FinalTotalAmount == 0 ? 0 : (TotalAdvancePaymentAmount / FinalTotalAmount) * 100;
    public decimal? TotalDailyLatenessPenalty => Contracts.Count == 0
        ? null
        : Contracts.Sum(x => x.DailyLatenessPenalty) / Contracts.Count;
    public decimal? TotalWorkDonePercent => Contracts.Count == 0
        ? null
        : Contracts.Sum(x => x.WorkDonePercent) / Contracts.Count;
    public decimal? TotalWorkDeliveryPercent => Contracts.Count == 0
        ? null
        : Contracts.Sum(x => x.WorkDeliveryPercent) / Contracts.Count;
    public decimal? TotalWorkCompletionPercent => Contracts.Count == 0
        ? null
        : Contracts.Sum(x => x.WorkCompletionPercent) / Contracts.Count;
    public required List<GetsFilteredContractorContractModel> Contracts { get; set; }

    public bool CanReview => Status == ContractorContractStatus.ManagementConfirmed && ReviewAble ? true : false;
}

public record GetsFilteredContractorContractModel
{
    public long Id { get; set; }
    public ContractorContractType ContractorContractTypeId { get; set; }
    public string? ContractorContractTypeName => ContractorContractTypeId.GetEnumDescription();
    public ContractorContractType ContractorContractTypeCode => ContractorContractTypeId;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public long? ProjectManagerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? PercentageDoingJobWell { get; set; }
    public decimal? DoingJobWellAmount { get; set; }
    public decimal? PercentageAdvancePayment { get; set; }
    public decimal? AdvancePaymentAmount { get; set; }
    public decimal? DailyLatenessPenalty { get; set; }
    public decimal? WorkDonePercent { get; set; }
    public decimal? WorkDeliveryPercent { get; set; }
    public decimal? WorkCompletionPercent { get; set; }
    public string? Description { get; set; }
}
