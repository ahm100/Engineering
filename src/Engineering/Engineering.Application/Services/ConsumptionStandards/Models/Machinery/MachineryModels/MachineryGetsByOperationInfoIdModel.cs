namespace Engineering.Application.Services.ConsumptionStandards.Models.Machinery.MachineryModels;

public record MachineryGetsByOperationInfoIdModel(
    long OperationInfoMachineryId,
    long Id,
    string? MachineryName,
    string? MachineryCode,
    decimal MachineryNumber,
    string TimeSpant,
    decimal? UnusedPercentage
    );
