using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.CreateMachineryConsumable;

public record CreateConsumableVolumeMachineryRequest(
    long ProjectOperationDetailId,
    long MachineryId,
    decimal? Number,
    decimal? UnusedPercentage,
    RequestMachineryUnit? Unit,
    string FinalValue
     ) : IHttpRequest;
