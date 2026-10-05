namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DeleteMachineryConsumableVolume;

public record DeleteConsumableVolumeMachineryRequest(
    long Id
     ) : IHttpRequest;
