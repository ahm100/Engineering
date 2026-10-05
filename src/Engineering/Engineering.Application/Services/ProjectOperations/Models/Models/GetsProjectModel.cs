using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.Models;

public record GetsProjectOperationByProjectModel
{
    public long Id { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public bool HaveStandard { get; set; }
    public long OperationInfoMeasurementId { get; set; }
    public string? OperationInfoMeasurementName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public bool? HaveEmployerContract { get; set; }
    public long? EmployerContractId { get; set; }
    public string? ContractCode { get; set; } = string.Empty;
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public decimal Workload { get; set; }
    public decimal TolerancePercentage { get; set; }
    public decimal? Price { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ChangedPrice { get; set; }
    public int? Priority { get; set; }
    public ProjectOperationStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public long? DependencyId { get; set; }
    public bool? IsDefaultRelation { get; set; }
    public long? RelationId { get; set; }
    public string? RelationName { get; set; } = string.Empty;
    public string? RelationCode { get; set; } = string.Empty;
    public long? RelationMeasurementId { get; set; }
    public string? RelationMeasurementName { get; set; } = string.Empty;
    public int? RelationDays { get; set; }
    public ProjectOperationDependencyType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public DateTime? StartDate { get; set; }
    public string? ShamsiStartDate => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? ShamsiEndDate => TimeCalculator.ConvertToShamsi(StartDate);
    public List<string>? Urls { get; set; }
    public bool GoodsInProgress { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public string? ShamsiCreated => TimeCalculator.ConvertToShamsi(Created);
    public OperationInfoDataModel OperationInfoModel { get; set; } = new();
    public bool CanAssignContractor { get; set; }
    public bool CanCreateOperationService { get; set; }
}

public record OperationInfoDataModel
{
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;

}