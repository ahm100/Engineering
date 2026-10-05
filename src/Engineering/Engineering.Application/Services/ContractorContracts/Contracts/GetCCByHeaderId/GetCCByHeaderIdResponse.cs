using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetCCByHeaderId;

public record GetCCByHeaderIdResponse
(
    List<GetCCByHeaderIdModel> Data,
    int RowCount
    );

public class GetCCByHeaderIdModel
{
    public long Id { get; set; }
    public ContractorContractType ContractorContractTypeId { get; set; }
    public string? ContractorContractTypeName => ContractorContractTypeId.GetEnumDescription();
    public ContractorContractType ContractorContractTypeCode => ContractorContractTypeId;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long ContractorId { get; set; }
    public long CurrencyId { get; set; }
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
    public List<GetCCByHeaderIdDetailCostOverModel> CostOvers { get; set; } = new();
    public List<GetCCByHeaderIdDetailedModel> Details { get; set; } = new();
}

public class GetCCByHeaderIdDetailedModel
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
    public List<GetCCByHeaderIdDetailPriceModel> Prices { get; set; } = new();
    public List<GetCCByHeaderIdDetailCostOverModel> CostOvers { get; set; } = new();
    public List<GetCCByHeaderIdDetailServiceModel> Services { get; set; } = new();
}

public class GetCCByHeaderIdDetailCostOverModel
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

public class GetCCByHeaderIdDetailPriceModel
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public long CurrencyId { get; set; }
    public bool IsActive { get; set; }
    public string? Currency { get; set; } = string.Empty;
}

public class GetCCByHeaderIdDetailServiceModel
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