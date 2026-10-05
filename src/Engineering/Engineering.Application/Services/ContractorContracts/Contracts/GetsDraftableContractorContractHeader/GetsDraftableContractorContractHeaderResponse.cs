
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsDraftableContractorContractHeader;

public record GetsDraftableContractorContractHeaderResponse(
    List<GetsDraftableContractorContractHeaderModel> Data,
    int RowCount
    );

public record GetsDraftableContractorContractHeaderModel
{
    public long Id { get; set; }
    public long ContractorId { get; set; }
    public string? Contractor { get; set; }
    public Guid? ContractorPreferentialCode { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public string? CostCenterCode { get; set; }
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public Guid? ProjectPreferentialCode { get; set; }
    public long? ProjectManagerId { get; set; }
    public string? ProjectManager { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? FinalTotalAmount => ContractorContracts.Sum(x => x.TotalAmount);

    public decimal? TotalPercentageDoingJobWell =>
        ContractorContracts.Count > 0
            ? ContractorContracts.Sum(x => x.PercentageDoingJobWell) / ContractorContracts.Count
            : null;

    public decimal? TotalDoingJobWellAmount =>
        ContractorContracts.Count > 0
            ? ContractorContracts.Sum(x => x.DoingJobWellAmount) / ContractorContracts.Count
            : null;

    public decimal? TotalAdvancePaymentAmount =>
        ContractorContracts.Sum(x => x.AdvancePaymentAmount);

    public decimal? TotalPercentageAdvancePayment =>
        FinalTotalAmount > 0
            ? (TotalAdvancePaymentAmount / FinalTotalAmount) * 100
            : null;

    public decimal? TotalDailyLatenessPenalty =>
        ContractorContracts.Count > 0
            ? ContractorContracts.Sum(x => x.DailyLatenessPenalty) / ContractorContracts.Count
            : null;

    public decimal? TotalWorkDonePercent =>
        ContractorContracts.Count > 0
            ? ContractorContracts.Sum(x => x.WorkDonePercent) / ContractorContracts.Count
            : null;

    public decimal? TotalWorkDeliveryPercent =>
        ContractorContracts.Count > 0
            ? ContractorContracts.Sum(x => x.WorkDeliveryPercent) / ContractorContracts.Count
            : null;

    public decimal? TotalWorkCompletionPercent =>
        ContractorContracts.Count > 0
            ? ContractorContracts.Sum(x => x.WorkCompletionPercent) / ContractorContracts.Count
            : null;

    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string? Description { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public List<GetsDraftableContractorContractTypeModel> ContractorContracts { get; set; } = new();
}

public record GetsDraftableContractorContractTypeModel
{
    public long Id { get; set; }
    public ContractorContractType ContractorContractTypeId { get; set; }
    public string? ContractorContractTypeName => ContractorContractTypeId.GetEnumDescription();
    public ContractorContractType ContractorContractTypeCode => ContractorContractTypeId;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? TotalAmount { get; set; } = 0;
    public decimal? PercentageDoingJobWell { get; set; }
    public decimal? DoingJobWellAmount { get; set; }
    public decimal? PercentageAdvancePayment { get; set; }
    public decimal? AdvancePaymentAmount { get; set; }
    public decimal? DailyLatenessPenalty { get; set; }
    public int? WorkDonePercent { get; set; }
    public int? WorkDeliveryPercent { get; set; }
    public int? WorkCompletionPercent { get; set; }
    public string? Description { get; set; }
}
