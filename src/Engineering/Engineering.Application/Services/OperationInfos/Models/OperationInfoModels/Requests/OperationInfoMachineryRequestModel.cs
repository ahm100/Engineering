namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;

public record OperationInfoMachineryRequestModel(
    long Id,
    int MachineryNumber,
    string TimeSpant,
    decimal? UnusedPercentage
 );
