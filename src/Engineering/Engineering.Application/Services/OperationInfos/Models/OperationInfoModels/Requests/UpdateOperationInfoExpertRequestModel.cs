namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;

public record UpdateOperationInfoExpertRequestModel(
    long? OperationInfoExpertId,
    long Id,
    int ExpertNumber,
    string TimeSpant,
    decimal? UnusedPercentage,
    bool? IsDeleted
 );
