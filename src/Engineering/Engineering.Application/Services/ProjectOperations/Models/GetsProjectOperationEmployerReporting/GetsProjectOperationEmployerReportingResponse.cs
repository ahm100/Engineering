
namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReporting;

public record GetsProjectOperationEmployerReportingResponse(
    List<GetsProjectOperationEmployerReportingModel> Data,
    int RowCount);

public record GetsProjectOperationEmployerReportingModel
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public long OperationInfoMeasurementId { get; set; }
    public string? OperationInfoMeasurementName { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public DateTime? StartDate { get; set; }
    public string? ShamsiStartDate => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? ShamsiEndDate => TimeCalculator.ConvertToShamsi(EndDate);
    public decimal Workload { get; set; }
    public decimal DoneWorkload => DailyProjectOperations.Sum(x => x.Volume);
    public decimal RemaindedWorkload => Workload - DoneWorkload;
    public string? Description { get; set; } = string.Empty;
    public List<DailyProjectOperationModel>? DailyProjectOperations { get; set; } = new List<DailyProjectOperationModel>();

}

public record DailyProjectOperationModel
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public string? OperationInfoMeasurementName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public string? Location { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal Volume => Length * Width * Height * Weight * Number;
    public string? Description { get; set; } = string.Empty;
}