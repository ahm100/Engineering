
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToRequestResended;

public record ChangeToRequestResendedTransportationRequestRequest(
    long Id,
    string ManagerDescription
     ) : IHttpRequest;
