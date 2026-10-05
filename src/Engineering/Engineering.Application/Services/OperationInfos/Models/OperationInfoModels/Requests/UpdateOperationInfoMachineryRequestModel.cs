namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;

public record UpdateOperationInfoMachineryRequestModel(
    long? OperationInfoMachineryId,
    long Id,
    int MachineryNumber,
    string TimeSpant,
    decimal? UnusedPercentage,
    bool? IsDeleted
 );
