
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToRequestRejection;

public record ChangeToRequestRejectionTransportationRequestRequest(
    long Id,
    string ManagerDescription
     ) : IHttpRequest;
