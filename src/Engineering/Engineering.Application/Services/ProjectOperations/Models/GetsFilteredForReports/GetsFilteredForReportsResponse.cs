namespace Engineering.Application.Services.ProjectOperations.Models.GetsFilteredForReports;

public record GetsFilteredForReportsResponse(
    List<GetsFilteredForReportsModel> Data,
    int RowCount);

public record GetsFilteredForReportsModel()
{
    public long ProjectOperationId { get; set; }
    public long Id { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public string? OperationLatinName { get; set; }
    public long UnitOfMeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}