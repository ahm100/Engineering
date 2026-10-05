namespace Engineering.Application.Services.ServiceInfos.Models.GetActiveServices;

public record GetActiveServiceInfosRequest(
    string? FilterData,
    string? ServiceInfoCode,
    string? ServiceInfoName,
    long? ProjectId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
