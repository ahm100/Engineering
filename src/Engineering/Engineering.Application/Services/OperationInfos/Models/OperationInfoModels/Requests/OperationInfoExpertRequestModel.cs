namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;

public record OperationInfoExpertRequestModel(
    long Id,
    int ExpertNumber,
    string TimeSpant,
    decimal? UnusedPercentage
 );
