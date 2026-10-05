namespace Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationChild;

public record GetsOperationLocationChildRequest(
    long Id,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
