using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelExporter;

public record GetsProjectOperationReportingExcelExporterModel
{
    public long Id { get; set; }
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public long OperationInfoMeasurementId { get; set; }
    public string? OperationInfoMeasurementName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public string? CategoryName { get; set; } = string.Empty;
    public string? BranchName { get; set; } = string.Empty;
    public string? SeasonName { get; set; } = string.Empty;
    public string? Contractors { get; set; } = string.Empty;
    public string? ContractorsNickName { get; set; } = string.Empty;
    public string? ImplementationAssistants { get; set; } = string.Empty;
    public string? ImplementationAssistantsNickName { get; set; } = string.Empty;
    public string? TechnicalAssistants { get; set; } = string.Empty;
    public string? TechnicalAssistantsNickName { get; set; } = string.Empty;
    public string? DailyProjectOperationCreators { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public string? ShamsiStartDate => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? ShamsiEndDate => TimeCalculator.ConvertToShamsi(EndDate);
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public decimal TolerancePercentage { get; set; }
    public decimal? Price { get; set; }
    public int? Priority { get; set; }
    public ProjectOperationStatus Status { get; set; }
    public string? StatusDescription => Status.GetEnumDescription();
    public decimal Workload { get; set; }
    public decimal DoneWorkload { get; set; }
    public decimal RemaindedWorkload => Workload - DoneWorkload;
    public long CreatorId { get; set; }
    public string? CreatorName { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? ShamsiCreated => TimeCalculator.ConvertToShamsi(Created);
    public bool GoodsInProgress { get; set; }
    public string? Description { get; set; } = string.Empty;
    public List<long>? ImplementationAssistantIds { get; set; } = new List<long>();
    public List<long>? TechnicalAssistantIds { get; set; } = new List<long>();
    public List<long?> ContractorIds { get; set; } = new List<long?>();
    public List<long>? DailyCreatorsIds { get; set; } = new List<long>();
    public List<decimal>? DailyAmounts { get; set; } = new List<decimal>();
};
