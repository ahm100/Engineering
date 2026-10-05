namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.CreateExpertConsumableVolume;

public record CreateConsumableVolumeExpertRequest(
    long ProjectOperationDetailId,
    long ExpertId,
    decimal? Number,
    decimal? UnusedPercentage,
    string FinalValue
     ) : IHttpRequest;
