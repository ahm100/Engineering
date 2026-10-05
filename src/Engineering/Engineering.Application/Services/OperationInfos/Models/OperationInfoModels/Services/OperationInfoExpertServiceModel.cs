
namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Services;

public record OperationInfoExpertServiceModel(
long Id,
    string? Name,
    string? Code,
    int ExpertNumber,
    long TimeSpant,
    decimal? UnusedPercentage
 );
