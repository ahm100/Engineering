
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToAccepted;

public record ChangeToAcceptedTransportationRequestRequest(
    long Id,
    string? ManagerDescription,
    bool? PanelPaid
     ) : IHttpRequest;
