namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetsFilteredMachineriyVolume;

public record GetsFilteredMachineriyVolumeRequest(
    long ProjectId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    long? MachineryGroupId,
    long? MachineryId,
    string? FilterData
     ) : IHttpRequest;
