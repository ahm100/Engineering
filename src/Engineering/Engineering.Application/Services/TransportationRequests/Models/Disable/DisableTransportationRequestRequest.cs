
namespace Engineering.Application.Services.TransportationRequests.Models.Disable;

public record DisableTransportationRequestRequest(
    long Id
     ) : IHttpRequest;
