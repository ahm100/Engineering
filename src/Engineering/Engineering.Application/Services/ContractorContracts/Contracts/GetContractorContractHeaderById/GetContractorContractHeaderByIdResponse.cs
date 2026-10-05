using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;

public class GetContractorContractHeaderByIdResponse
{
    public long Id { get; set; }
    public long? VersionId { get; set; }
    public int Version { get; set; } = 0;
    public bool HaveAVersion { get; set; } = false;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public long? ProjectId => Contracts.FirstOrDefault()!.ProjectId;
    public string? ProjectName => Contracts.Select(x => x.ProjectName).Distinct().JoinList();
    public long ContractorId { get; set; }
    public string? Contractor { get; set; }
    public ContractorContractStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public long CurrencyId { get; set; }
    public string? Currency { get; set; }
    public DateTime? StartDate => Contracts.Min(x => x.StartDate);
    public DateTime? EndDate => Contracts.Max(x => x.EndDate);
    public decimal? FinalTotalAmount => Contracts.Sum(x => x.TotalAmount);
    public decimal? TotalPercentageDoingJobWell => (Contracts.Sum(x => x.PercentageDoingJobWell) / Contracts.Count);
    public decimal? TotalDoingJobWellAmount => (Contracts.Sum(x => x.DoingJobWellAmount) / Contracts.Count);
    public decimal? TotalAdvancePaymentAmount => Contracts.Sum(x => x.AdvancePaymentAmount);
    public decimal? TotalPercentageAdvancePayment => TotalAdvancePaymentAmount == 0 || FinalTotalAmount == 0 ? 0 : (TotalAdvancePaymentAmount / FinalTotalAmount) * 100;
    public decimal? TotalDailyLatenessPenalty => (Contracts.Sum(x => x.DailyLatenessPenalty) / Contracts.Count);
    public decimal? TotalWorkDonePercent => (Contracts.Sum(x => x.WorkDonePercent) / Contracts.Count);
    public decimal? TotalWorkDeliveryPercent => (Contracts.Sum(x => x.WorkDeliveryPercent) / Contracts.Count);
    public decimal? TotalWorkCompletionPercent => (Contracts.Sum(x => x.WorkCompletionPercent) / Contracts.Count);
    public string? Description { get; set; }
    public List<string>? Urls { get; set; }
    public required List<GetHeaderContractorContractModel> Contracts { get; set; } = new();
}

public class GetHeaderContractorContractModel
{
    public long Id { get; set; }
    public ContractorContractType ContractorContractTypeId { get; set; }
    public string? ContractorContractTypeName => ContractorContractTypeId.GetEnumDescription();
    public ContractorContractType ContractorContractTypeCode => ContractorContractTypeId;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
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
    public List<GetContractorContractHeaderByIdDetailCostOverModel> CostOvers { get; set; } = new();
    public List<GetContractorContractHeaderByIdDetailedModel> Details { get; set; } = new();
}

public class GetContractorContractHeaderByIdDetailedModel
{
    public long Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? UnitAmount { get; set; }
    public decimal? TotalAmount { get; set; }
    public long? ProjectOperationId => Services.FirstOrDefault()!.ProjectOperationId;
    public string? OperationInfoName => Services.Select(x => x.OperationInfoName).Distinct().JoinList();
    public string? OperationInfoCode => Services.Select(x => x.OperationInfoCode).Distinct().JoinList();
    public string? ProjectOperationUnitOfMeasurement => Services.Select(x => x.ProjectOperationUnitOfMeasurement).Distinct().JoinList();
    public decimal Workloads => Services.FirstOrDefault()!.Workload;
    public string? ServiceInfoUnitOfMeasurement => Services.Select(x => x.ServiceInfoUnitOfMeasurement).Distinct().JoinList();
    public bool IsProjectService => Services.All(x => x.ProjectServiceId.HasValue);
    public long ServiceInfoId => Services.FirstOrDefault()!.ServiceInfoId;
    public string? ServiceInfoName => Services.Select(x => x.ServiceInfoName).Distinct().JoinList();
    public string? ServiceInfoCode => Services.Select(x => x.ServiceInfoCode).Distinct().JoinList();
    public decimal? ServiceInfoVolume => IsProjectService ? Services.FirstOrDefault()!.ServiceInfoVolume : Services.Sum(x => x.ServiceInfoVolume);
    public decimal? FinalAmount => Services.Sum(x => x.FinalAmount);
    public List<GetContractorContractHeaderByIdDetailPriceModel> Prices { get; set; } = new();
    public List<GetContractorContractHeaderByIdDetailCostOverModel> CostOvers { get; set; } = new();
    public List<GetContractorContractHeaderByIdDetailServiceModel> Services { get; set; } = new();
}

public class GetContractorContractHeaderByIdDetailCostOverModel
{
    public long Id { get; set; }
    public long ContractorId { get; set; }
    public string? ContractorName { get; set; } = string.Empty;
    public long CostOverId { get; set; }
    public string? CostOverName { get; set; } = string.Empty;
    public string? CostOverCode { get; set; } = string.Empty;
    public decimal Percentage { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; } = string.Empty;
}

public class GetContractorContractHeaderByIdDetailPriceModel
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public long CurrencyId { get; set; }
    public bool IsActive { get; set; }
    public string? Currency { get; set; } = string.Empty;
}

public class GetContractorContractHeaderByIdDetailServiceModel
{
    public long Id { get; set; }
    public long? ProjectOperationDetailContractorServiceId { get; set; }
    public long? ProjectServiceId { get; set; }
    public long? ProjectServiceDetailId { get; set; }
    public long? ProjectOperationId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public long ProjectOperationUnitOfMeasurementId { get; set; }
    public string? ProjectOperationUnitOfMeasurement { get; set; } = string.Empty;
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; }
    public string? ServiceInfoCode { get; set; }
    public decimal? ServiceInfoVolume { get; set; } = 0;
    public decimal FinalAmount { get; set; }
    public long ServiceInfoUnitOfMeasurementId { get; set; }
    public string? ServiceInfoUnitOfMeasurement { get; set; } = string.Empty;
    public string? PrivateCode { get; set; }
    public string? PrivateName { get; set; }
    public string? PublicCode { get; set; }
    public string? PublicName { get; set; }
    public string? Description { get; set; }
    public decimal Workload { get; set; }
}