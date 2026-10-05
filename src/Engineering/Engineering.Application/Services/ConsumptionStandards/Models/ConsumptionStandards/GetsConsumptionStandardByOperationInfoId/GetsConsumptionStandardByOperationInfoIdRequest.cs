
namespace Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.GetsConsumptionStandardByOperationInfoId;

public record GetsConsumptionStandardByOperationInfoIdRequest(
    long OprationInfoId
     ) : IHttpRequest;
