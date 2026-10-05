namespace Engineering.Application.Services.ProjectOperations.Models.GetsFilteredByProjectIds;

public record GetsFilteredByProjectIdsResponse(
    List<GetsFilteredByProjectIdsModel> Data,
    int RowCount);

public record GetsFilteredByProjectIdsModel
{
    public long Id { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public long OperationInfoMeasurementId { get; set; }
    public string? OperationInfoMeasurementName { get; set; } = string.Empty;
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public decimal Workload { get; set; }

}