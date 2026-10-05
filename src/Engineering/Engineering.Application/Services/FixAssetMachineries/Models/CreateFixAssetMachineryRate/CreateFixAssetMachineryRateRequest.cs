namespace Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryRate;

public record CreateFixAssetMachineryRateRequest(
    long FixAssetMachineryId,
    List<CreateRateDataRequest>? Rates
     ) : IHttpRequest;

public record CreateRateDataRequest(
    DateTime StartDate,
    DateTime EndDate,
    TimeSpan? StartTime,
    TimeSpan? EndTime,
    decimal? HourlyRate,
    decimal? DailyRate,
    decimal? ServiceRate,
    decimal? VolumeRate
);