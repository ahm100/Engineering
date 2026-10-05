namespace Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachineryRate;

public record UpdateFixAssetMachineryRateRequest(
    long Id,
    DateTime StartDate,
    DateTime EndDate,
    TimeSpan? StartTime,
    TimeSpan? EndTime,
    decimal? HourlyRate,
    decimal? DailyRate,
    decimal? ServiceRate,
    decimal? VolumeRate
     ) : IHttpRequest;
