namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyOperationCreatedByProjectReport;

public record GetsDailyOperationCreatedByProjectReportResponse(
    List<GetsDailyOperationCreatedByProjectReportModel> Data,
    int RowCount);

public class GetsDailyOperationCreatedByProjectReportModel
{
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public decimal DailyOperationCount { get; set; }
}
