using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.UpdateMachineryConsumableVolume;

public record UpdateConsumableVolumeMachineryRequest(
    long Id,
    long MachineryId,
    decimal? Number,
    decimal? UnusedPercentage,
    RequestMachineryUnit? Unit,
    string FinalValue
     ) : IHttpRequest;
