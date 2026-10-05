namespace Engineering.Application.Services.ConsumptionStandards.Models.Experts.CreateExpert;

public record CreateConsumptionStandardExpertRequest(
    long Id,
    int ExpertNumber,
    string TimeSpant,
    decimal? UnusedPercentage,
    long OperationInfoId
     ) : IHttpRequest;
