using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractDetailReports;

public record GetsFilteredContractorContractDetailReportsModel
{
    public long ContractorContractId { get; set; }
    public long Id { get; set; }
    public decimal? WorkLoad { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public decimal? UnitAmount { get; set; }
    public decimal? TotalAmount { get; set; }
    public long? ProjectOperationId { get; set; }
    public long? OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public long? ServiceInfoUnitOfMeasurementId { get; set; }
    public string? ServiceInfoUnitOfMeasurement { get; set; } = string.Empty;
    public long? ProjectOperationDetailId { get; set; }
    public long? OperationLocationId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public List<ContractorContractCostCenterModel> CostCenters { get; set; } = [];
    public ProjectOperationDetailStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
}

public record ContractorContractCostCenterModel
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}