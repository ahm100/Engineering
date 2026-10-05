namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsTotalInspectionReport;

public record GetsTotalInspectionReportResponse
{
    public decimal? TotalLengths { get; set; } = 0;
    public decimal? TotalWidths { get; set; } = 0;
    public decimal? TotalHeights { get; set; } = 0;
    public decimal? TotalWeights { get; set; } = 0;
    public decimal? TotalNumbers { get; set; } = 0;
    public decimal? TotalAmounts { get; set; } = 0;
}
