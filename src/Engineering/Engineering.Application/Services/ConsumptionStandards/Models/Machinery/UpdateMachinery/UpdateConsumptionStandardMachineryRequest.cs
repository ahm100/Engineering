namespace Engineering.Application.Services.ConsumptionStandards.Models.Machinery.UpdateMachinery;

public record UpdateConsumptionStandardMachineryRequest(
    long OperationInfoMachineryId,
    long Id,
    int MachineryNumber,
    string TimeSpant,
    decimal? UnusedPercentage
     ) : IHttpRequest;