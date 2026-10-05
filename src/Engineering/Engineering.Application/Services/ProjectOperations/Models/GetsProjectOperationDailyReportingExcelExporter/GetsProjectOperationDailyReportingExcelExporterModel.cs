namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReportingExcelExporter;

public record GetsProjectOperationDailyReportingExcelExporterModel
{
    public long Id { get; set; }
    public long CostCenterId { get; set; }
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
    public string RialPrice { get; set; } = "0";
    public string DollarPrice { get; set; } = "0";
    public string TotalRialPrice { get; set; } = "0";
    public string TotalDollarPrice { get; set; } = "0";
    public decimal Workload { get; set; }
    public decimal DoneWorkload => ProjectOperationDailyDetail!.Sum(s => s.Volume);
    public decimal RemaindedWorkload => Workload - DoneWorkload;
    public string? Description { get; set; } = string.Empty;
    public List<ProjectOperationDailyModel>? ProjectOperationDailyDetail { get; set; } = new List<ProjectOperationDailyModel>();
}

public record ProjectOperationDailyModel
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