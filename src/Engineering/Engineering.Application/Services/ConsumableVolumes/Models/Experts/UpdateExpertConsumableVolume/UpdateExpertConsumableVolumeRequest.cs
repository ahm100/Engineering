namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.UpdateExpertConsumableVolume;

public record UpdateConsumableVolumeExpertRequest(
    long Id,
    long ExpertId,
    decimal? Number,
    decimal? UnusedPercentage,
    string FinalValue
     ) : IHttpRequest;
