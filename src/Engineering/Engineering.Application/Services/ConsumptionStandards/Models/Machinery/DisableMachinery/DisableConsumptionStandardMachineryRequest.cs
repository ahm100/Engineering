namespace Engineering.Application.Services.ConsumptionStandards.Models.Machinery.DisableMachinery;

public record DisableConsumptionStandardMachineryRequest(
    long OperationInfoMachineryId
     ) : IHttpRequest;
