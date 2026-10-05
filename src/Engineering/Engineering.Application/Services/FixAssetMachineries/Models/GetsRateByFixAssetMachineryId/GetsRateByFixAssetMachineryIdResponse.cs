namespace Engineering.Application.Services.FixAssetMachineries.Models.GetsRateByFixAssetMachineryId;

public record GetsRateByFixAssetMachineryIdResponse(
    List<GetsRateByFixAssetMachineryIdModel> Data,
    int RowCount);

public record GetsRateByFixAssetMachineryIdModel
{
    public long Id { get; set; }
    public long MachineryId { get; set; }
    public string MachineryName { get; set; } = string.Empty;
    public string MachineryCode { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public TimeSpan? StartTime { get; set; }
    public DateTime EndDate { get; set; }
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public TimeSpan? EndTime { get; set; }
    public decimal? HourlyRate { get; set; }
    public decimal? DailyRate { get; set; }
    public decimal? ServiceRate { get; set; }
    public decimal? VolumeRate { get; set; }
}
