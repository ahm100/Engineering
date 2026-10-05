namespace Engineering.Application.Services.ConsumptionStandards.Models.Machinery.MachineryGetsByOprationInfoId;

public record MachineryGetsByOprationInfoIdRequest(
    long OprationInfoId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
