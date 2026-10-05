namespace Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestHistory;

public record GetsTransportationRequestHistoryRequest(
    long Id,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
