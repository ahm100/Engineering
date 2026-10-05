
namespace Engineering.Application.Services.TransportationRequests.Models.GetsFilteredRequester;

public record GetsFilteredRequesterRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
