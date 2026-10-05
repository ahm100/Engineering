namespace Engineering.Application.Services.ProjectOperations.Models.GetFltrPOForReports;

public record GetFltrPOForReportsResponse(
    List<GetFltrPOForReportsModel> Data,
    int RowCount);

public record GetFltrPOForReportsModel()
{
    public long Id { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public string? OperationLatinName { get; set; }
    public long UnitOfMeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}