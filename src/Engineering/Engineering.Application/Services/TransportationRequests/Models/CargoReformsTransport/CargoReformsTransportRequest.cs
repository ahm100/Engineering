namespace Engineering.Application.Services.TransportationRequests.Models.CargoReformsTransport;

public record CargoReformsTransportRequest(
    List<long> PalletIds,
    long Id
     ) : IHttpRequest;
