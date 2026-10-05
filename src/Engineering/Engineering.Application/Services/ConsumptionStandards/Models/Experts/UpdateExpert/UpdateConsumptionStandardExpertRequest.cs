namespace Engineering.Application.Services.ConsumptionStandards.Models.Experts.UpdateExpert;

public record UpdateConsumptionStandardExpertRequest(
    long OperationInfoExpertId,
    long Id,
    int ExpertNumber,
    string TimeSpant,
    decimal? UnusedPercentage
     ) : IHttpRequest;
