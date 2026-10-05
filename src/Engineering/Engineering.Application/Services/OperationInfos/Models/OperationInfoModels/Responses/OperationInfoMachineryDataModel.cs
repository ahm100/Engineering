namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Responses;

public record OperationInfoMachineryDataModel(
    long OperationInfoMachineryId,
    long Id,
    string? MachineryName,
    string? MachineryCode,
    decimal MachineryNumber,
    string TimeSpant,
    decimal? UnusedPercentage
 );
