namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyServiceInfo;

public record GetsDailyServiceInfoResponse(
    List<GetsDailyServiceInfoModel> Data,
    int RowCount
    );

public record GetsDailyServiceInfoModel
{
    public long Id { get; set; }
    public long? UnitOfMeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public decimal? Volume { get; set; }
}
