
namespace Engineering.Application.Services.TransportationRequests.Models.TransportationRequestGroupDelete;

public record TransportationRequestGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
