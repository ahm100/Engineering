namespace Engineering.Application.Services.ConsumptionStandards.Models.Machinery.DisableMachinery;

public record DisableConsumptionStandardMachineryResponse(
    long OperationInfoMachineryId,
    bool isDeleted
    );
