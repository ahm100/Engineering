namespace Engineering.Application.Services.TransportationRequests.Models.PackingReleaseFromTransport;

public record PackingReleaseFromTransportRequest(
    List<long> CargoIds,
    long Id
     ) : IHttpRequest;
