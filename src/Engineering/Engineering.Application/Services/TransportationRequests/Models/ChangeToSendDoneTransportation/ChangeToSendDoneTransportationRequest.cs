
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToSendDoneTransportation;

public record ChangeToSendDoneTransportationRequest(
    long Id,
    string? ManagerDescription
     ) : IHttpRequest;
