
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToPending;

public record ChangeToPendingTransportationRequestRequest(
    long Id
     ) : IHttpRequest;
