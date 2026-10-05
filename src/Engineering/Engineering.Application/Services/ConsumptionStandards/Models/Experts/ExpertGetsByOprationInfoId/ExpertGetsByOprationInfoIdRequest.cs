namespace Engineering.Application.Services.ConsumptionStandards.Models.Experts.ExpertGetsByOprationInfoId;

public record ExpertGetsByOprationInfoIdRequest(
    long OprationInfoId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
