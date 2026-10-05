namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Responses;

public record OperationInfoExpertDataModel(
    long OperationInfoExpertId,
    long Id,
    string? Name,
    string? Code,
    decimal ExpertNumber,
    string TimeSpant,
    decimal? UnusedPercentage
 );
