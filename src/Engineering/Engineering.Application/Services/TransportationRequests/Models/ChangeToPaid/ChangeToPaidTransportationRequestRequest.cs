
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToPaid;

public record ChangeToPaidTransportationRequestRequest(
    long Id,
    string ManagerDescription
     ) : IHttpRequest;
