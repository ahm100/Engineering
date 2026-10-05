
namespace Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceByProjectId;

public record GetsOperationInfoServiceByProjectIdResponse(
    List<GetsOperationInfoServiceByProjectIdModel> Data,
    int RowCount);

public record GetsOperationInfoServiceByProjectIdModel
{
    public long Id { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public long OperationInfoMeasurementId { get; set; }
    public string? OperationInfoMeasurementName { get; set; } = string.Empty;
    public string? TimeSpant { get; set; } = string.Empty;
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
};
