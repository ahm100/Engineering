namespace Engineering.Application.Services.ConsumptionStandards.Models.Experts.DisableExpert;

public record DisableConsumptionStandardExpertRequest(
    long OperationInfoExpertId
     ) : IHttpRequest;
